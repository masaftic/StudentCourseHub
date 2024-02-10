using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCourseHub.Models.DTO;

public class UpdateCourseRequestDto
{
    [Required(ErrorMessage = "Name is required")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public required string Description { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Credits must be a positive number")]
    public int Credits { get; set; }
}
