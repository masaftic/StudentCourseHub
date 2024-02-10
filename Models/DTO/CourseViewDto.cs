using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentCourseHub.Models.Domain;

namespace StudentCourseHub.Models.DTO;

public class CourseViewDto
{
    public int CourseId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int Credits { get; set; }
    public required IEnumerable<StudentViewSummaryDto> Students { get; set; }
}
