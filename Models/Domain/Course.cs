using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCourseHub.Models.Domain;

public class Course
{
    public int CourseId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int Credits { get; set; }
    public bool IsDeleted { get; set; }
    public required ICollection<Student> students { get; set; }
}