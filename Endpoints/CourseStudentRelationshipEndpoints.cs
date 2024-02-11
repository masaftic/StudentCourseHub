using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentCourseHub.Services;

namespace StudentCourseHub.Endpoints;

public static class CourseStudentRelationshipEndpoints
{
    public static void MapCourseStudentRelationshipEndpoints(this IEndpointRouteBuilder app)
    {
        var routes = app.MapGroup("")
            .WithOpenApi()
            .WithTags("StudentCourseRelationship");

		routes.MapPost("courses/{courseId}/students/{studentId}", async (int courseId, int studentId, CourseService courseService, StudentService studentService) =>
        {
            if (!await courseService.IsFound(courseId))
            {
                return Results.Problem($"Course with ID {courseId} not found, No action taken", statusCode: 404);
            }
            if (!await studentService.IsFound(studentId))
            {
                return Results.Problem($"Student with ID {studentId} not found, No action taken", statusCode: 404);
            }
            
            await courseService.AddStudentToCourse(studentId, courseId);
            
            var message = $"Student with ID {studentId} successfully added to Course with ID {courseId}.";
            return Results.Ok(new { message });
        })
        .WithSummary("Add Student to Course")
        .ProducesProblem(404)
        .Produces(StatusCodes.Status201Created);


		routes.MapDelete("courses/{courseId}/students/{studentId}", async (int courseId, int studentId, CourseService courseService, StudentService studentService) =>
        {
            if (!await courseService.IsFound(courseId))
            {
                return Results.Problem($"Course with ID {courseId} not found, No action taken", statusCode: 404);
            }
            if (!await studentService.IsFound(studentId))
            {
                return Results.Problem($"Student with ID {studentId} not found, No action taken", statusCode: 404);
            }
            
            if (!await courseService.RemoveStudentFromCourse(studentId, courseId))
            {
                return Results.Problem($"Student with ID {studentId} is not enrolled in Course with ID {courseId}.", statusCode: 400);
            }
            
            var message = $"Student with ID {studentId} successfully removed from Course with ID {courseId}.";
            return Results.Ok(new { message });
        })
        .WithSummary("Remove Student from Course")
        .ProducesProblem(404)
        .ProducesProblem(400)
        .Produces(StatusCodes.Status204NoContent);

    }
}