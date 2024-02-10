using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using StudentCourseHub.Models.DTO;
using StudentCourseHub.Services;

namespace StudentCourseHub.Endpoints;

public static class Students
{
    public static void MapStudentEndpoints(this IEndpointRouteBuilder app)
    {
        
        var routes = app.MapGroup("/students")
            .WithParameterValidation()
            .WithOpenApi()
            .WithTags("Students");


        routes.MapPost("/", async (CreateStudentRequestDto request, StudentService service) =>
        {
            try
            {
                int id = await service.CreateStudent(request);
                return Results.Created($"/students/{id}", id);
            }
            catch (Exception)
            {
                return Results.Problem("An unexpected error occurred while processing the request.",
                                       statusCode: StatusCodes.Status500InternalServerError);
            }
        })
        .WithSummary("Create Student")
        .ProducesProblem(500)
        .Produces(StatusCodes.Status201Created);


        routes.MapGet("/{id}", async (int id, StudentService service) =>
        {
            var student = await service.GetStudent(id);
            return student is null
                ? Results.Problem($"Student with ID {id} not found", statusCode: 404)
                : Results.Ok(student);
        })
        .WithSummary("Get Student")
        .ProducesProblem(404)
        .Produces<StudentViewDto>();


        routes.MapGet("/", async (StudentService service) =>
        {
            var student = await service.GetAllStudents();
            return Results.Ok(student);
        })
        .WithSummary("Get All Students")
        .Produces<List<StudentViewDto>>();

    }
}