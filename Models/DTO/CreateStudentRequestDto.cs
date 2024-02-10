using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using StudentCourseHub.Models.Domain;

namespace StudentCourseHub.Models.DTO;

public class CreateStudentRequestDto
{
    public int StudentId { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public required string Email { get; set; }

    [Range(13, 100, ErrorMessage = "Age must be between 13 and 100")]
    public int Age { get; set; }
}