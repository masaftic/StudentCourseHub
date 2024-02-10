using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCourseHub.Models.Domain;

public class Student
{
    public int StudentId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public int Age { get; set; }
    public bool IsDeleted { get; set; }
    public required ICollection<Course> Courses { get; set; }
}