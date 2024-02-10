using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using StudentCourseHub.Models.DTO;
using StudentCourseHub.Services;

namespace StudentCourseHub.Endpoints;

public static class CoursesEndpoints
{
    public static void MapCoursesEndpoints(this IEndpointRouteBuilder app)
    {

        var routes = app.MapGroup("/courses")
            .WithParameterValidation()
            .WithOpenApi()
            .WithTags("Courses");



        routes.MapGet("/", async (CourseService service) =>
        {
            var course = await service.GetAllCourses();
            return Results.Ok(course);
        })
        .WithSummary("Get All Courses")
        .Produces<List<CourseViewDto>>();
        

        routes.MapGet("/{id}", async (int id, CourseService service) =>
        {
            var Course = await service.GetCourse(id);
            return Course is null
                ? Results.Problem($"Course with ID {id} not found", statusCode: 404)
                : Results.Ok(Course);
        })
        .WithSummary("Get Course")
        .ProducesProblem(404)
        .Produces<CourseViewDto>();


        routes.MapPost("/", async (CreateCourseRequestDto request, CourseService service) =>
        {
            try
            {
                int id = await service.CreateCourse(request);
                return Results.Created($"/Courses/{id}", request);
            }
            catch (Exception)
            {
                return Results.Problem("An unexpected error occurred while processing the request.",
                                        statusCode: StatusCodes.Status500InternalServerError);
            }
        })
        .WithSummary("Create Course")
        .ProducesProblem(500)
        .Produces(StatusCodes.Status201Created);


        routes.MapPut("/{id}", async (int id, UpdateCourseRequestDto request, CourseService service) =>
        {
            bool isUpdated = await service.UpdateCourse(id, request);
            if (isUpdated)
            {
                var message = $"Course with ID {id} has been successfully updated.";
                return Results.Ok(new { message });
            }
            return Results.Problem($"Course with ID {id} not found. No action taken.", statusCode: 404);
        })
        .WithSummary("Update Course")
        .ProducesProblem(404)
        .Produces(200);


        routes.MapDelete("/{id}", async (int id, CourseService service) =>
        {
            var IsDeleted = await service.DeleteCourse(id);
            if (IsDeleted)
            {
                var message = $"Course with ID {id} has been successfully deleted.";
                return Results.Ok(new { message });
            };
            return Results.Problem($"Course with ID {id} not found, No action taken", statusCode: 404);
        })
        .WithSummary("Delete Course")
        .ProducesProblem(404)
        .Produces(200);


    }

}
