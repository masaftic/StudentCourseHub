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
                return Results.Created($"/students/{id}", request);
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


        routes.MapDelete("/{id}", async (int id, StudentService service) =>
        {
            var IsDeleted = await service.DeleteStudent(id);
            if (IsDeleted)
            {
                var message = $"Student with ID {id} has been successfully deleted.";
                return Results.Ok(new { message });
            };
            return Results.Problem($"Student with ID {id} not found, No action taken", statusCode: 404);
        })
        .WithSummary("Delete Student")
        .ProducesProblem(404)
        .Produces(200);


        routes.MapPut("/{id}", async (int id, UpdateStudentRequestDto request, StudentService service) =>
        {
            bool isUpdated = await service.UpdateStudent(id, request);
            if (isUpdated)
            {
                var message = $"Student with ID {id} has been successfully updated.";
                return Results.Ok(new { message });
            }
            return Results.Problem($"Student with ID {id} not found. No action taken.", statusCode: 404);
        })
        .WithSummary("Update Student")
        .ProducesProblem(404)
        .Produces(200);
    }
}