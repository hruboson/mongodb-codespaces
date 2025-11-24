using StudentApi.Models;
using StudentApi.Services;

namespace StudentApi.Endpoints;

public static class StudentEndpoint
{
    private static string route = "/students";

    public static IEndpointRouteBuilder MapStudents(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup(route);

        group.MapGet("/", async (StudentService service) =>
        {
            var list = await service.GetAsync();
            return Results.Ok(list);
        });

        group.MapGet("/{id}", async (StudentService service, string id) =>
        {
            var s = await service.GetAsync(id);
            return s != null ? Results.Ok(s) : Results.NotFound();
        });

        group.MapPost("/", async (StudentService service, Student s) =>
        {
            await service.CreateAsync(s);
            return Results.Created($"{route}/{s.Id}", s);
        });

        group.MapPut("/", async (StudentService service, Student s, string id) =>
        {
            var student = await service.GetAsync(id);

            if (student is null)
                return Results.NotFound();

            s.Id = id;

            await service.UpdateAsync(id, s);
            return Results.NoContent();
        });

        group.MapDelete("/", async (StudentService service, string id) =>
        {
            var student = await service.GetAsync(id);

            if (student is null)
                return Results.NotFound();

            await service.RemoveAsync(id);
            return Results.NoContent();
        });

        return routes;
    }
}