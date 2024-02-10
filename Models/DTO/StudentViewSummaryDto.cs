using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCourseHub.Models.DTO;

public class StudentViewSummaryDto
{
    public int StudentId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}
