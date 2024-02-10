using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentCourseHub.Data;
using StudentCourseHub.Models.Domain;
using StudentCourseHub.Models.DTO;

namespace StudentCourseHub.Services;

public class StudentService
{
    private readonly AppDbContext _context;

    public StudentService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<int> CreateStudent(CreateStudentRequestDto request)
    {
        var student = new Student
        {
            StudentId = request.StudentId,
            Name = request.Name,
            Email = request.Email,
            Age = request.Age,
            IsDeleted = false,
            Courses = []
        };

        _context.Add(student);
        await _context.SaveChangesAsync();

        return student.StudentId;
    }

    public async Task<StudentViewDto?> GetStudent(int id)
    {
        return await _context.Students
            .Where(x => x.StudentId == id)
            .Where(x => !x.IsDeleted)
            .Select(x => new StudentViewDto
            {
                StudentId = x.StudentId,
                Name = x.Name,
                Email = x.Email,
                Age = x.Age,
                Courses = x.Courses
                .Select(course => new CourseViewSummaryDto
                {
                    CourseId = course.CourseId,
                    Name = course.Name,
                    Credits = course.Credits,
                })
            })
            .SingleOrDefaultAsync();
    }

    public async Task<List<StudentViewDto>> GetAllStudents()
    {
        return await _context.Students
            .Where(x => !x.IsDeleted)
            .Select(x => new StudentViewDto
            {
                StudentId = x.StudentId,
                Name = x.Name,
                Email = x.Email,
                Age = x.Age,
                Courses = x.Courses
                .Select(course => new CourseViewSummaryDto
                {
                    CourseId = course.CourseId,
                    Name = course.Name,
                    Credits = course.Credits,
                })
            })
            .ToListAsync();
    }
}
