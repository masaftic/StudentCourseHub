using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentCourseHub.Data;
using StudentCourseHub.Models.Domain;
using StudentCourseHub.Models.DTO;

namespace StudentCourseHub.Services;

public class CourseService
{
    private readonly AppDbContext _context;

    public CourseService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateCourse(CreateCourseRequestDto request)
    {
        var course = new Course
        {
            Name = request.Name,
            Description = request.Description,
            Credits = request.Credits,
            IsDeleted = false,
            Students = []
        };

        _context.Add(course);
        await _context.SaveChangesAsync();

        return course.CourseId;
    }

    public async Task<CourseViewDto?> GetCourse(int id)
    {
        return await _context.Courses
            .Where(x => x.CourseId == id)
            .Where(x => !x.IsDeleted)
            .Select(x => new CourseViewDto
            {
                CourseId = x.CourseId,
                Name = x.Name,
                Description = x.Description,
                Students = x.Students
                .Where(student => !student.IsDeleted)
                .Select(student => new StudentViewSummaryDto
                {
                    StudentId = student.StudentId,
                    Name = student.Name,
                    Email = student.Email
                }).ToList()
            })
            .SingleOrDefaultAsync();
    }

    public async Task<List<CourseViewDto>> GetAllCourses()
    {
        return await _context.Courses
            .Where(x => !x.IsDeleted)
            .Select(x => new CourseViewDto
            {
                CourseId = x.CourseId,
                Name = x.Name,
                Description = x.Description,
                Students = x.Students
                .Where(student => !student.IsDeleted)
                .Select(student => new StudentViewSummaryDto
                {
                    StudentId = student.StudentId,
                    Name = student.Name,
                    Email = student.Email
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<bool> DeleteCourse(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course is null || course.IsDeleted)
        {
            return false;
        }

        course.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateCourse(int id, UpdateCourseRequestDto request)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course is null || course.IsDeleted)
        {
            return false;
        }

        course.Name = request.Name;
        course.Description = request.Description;
        course.Credits = request.Credits;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task AddStudentToCourse(int studentId, int courseId)
    {
        Student student = (await _context.Students
            .Include(s => s.Courses)
            .FirstOrDefaultAsync(s => s.StudentId == studentId))!;

        Course course = (await _context.Courses
            .Include(c => c.Students)
            .FirstOrDefaultAsync(c => c.CourseId == courseId))!;

        course.Students.Add(student);
        student.Courses.Add(course);

        await _context.SaveChangesAsync();        
    }

    public async Task<bool> IsFound(int id)
    {
        return await _context.Courses
            .Where(x => !x.IsDeleted)
            .Where(x => x.CourseId == id)
            .AnyAsync();
    }

    public async Task<bool> RemoveStudentFromCourse(int studentId, int courseId)
    {
        Student student = (await _context.Students
            .Include(s => s.Courses)
            .FirstOrDefaultAsync(s => s.StudentId == studentId))!;

        Course course = (await _context.Courses
            .Include(c => c.Students)
            .FirstOrDefaultAsync(c => c.CourseId == courseId))!;

        if (!course.Students.Remove(student) || !student.Courses.Remove(course))
        {
            return false;
        }
        await _context.SaveChangesAsync();
        return true;  
    }
}
