using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentCourseHub.Models.Domain;

namespace StudentCourseHub.Models.DTO;

public class StudentViewDto
{
    public int StudentId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public int Age { get; set; }
    public required IEnumerable<CourseViewSummaryDto> Courses { get; set; }
}
