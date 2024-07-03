using PlooCinema.WebApi.Repositories;
using PlooCinema.WebApi.Model;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<MovieRepositoryJson>(ServiceProvider =>
{
    return new MovieRepositoryJson();
});

builder.Services.AddScoped<MovieRepositoryPostgresSql>(ServiceProvider =>
{
    var connString = ServiceProvider
        .GetRequiredService<IConfiguration>()
        .GetConnectionString("DefaultConnection");

    return new MovieRepositoryPostgresSql(connString);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PlooCinema.WebApi",
        Description = "Documentação da API PlooCinema.WebApi",
        Version = "v1"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opttions =>
    {
        opttions.SwaggerEndpoint("/swagger/v1/swagger.json", "PlooCinema.WebApi v1");
    });
}

app.MapGet("/json/movie", ([FromServices] MovieRepositoryJson repository,[FromQuery] string? name) =>
{
    if (string.IsNullOrEmpty(name))
        return Results.Ok(repository.SearchAll());

    return Results.Ok(repository.SearchByName(name));
});

app.MapGet("/json/movie/{id}", ([FromServices] MovieRepositoryJson repository, [FromRoute(Name = "id")] int id) =>
{
    var movie = repository.SearchById(id);

    if (movie == null)
        return Results.NotFound();

    return Results.Ok(movie);
});

app.MapPost("/json/movie", ([FromServices] MovieRepositoryJson repository, [FromBody] Movie movie) =>
{
    try
    {
        var created = repository.Create(movie);
        return Results.Created($"/json/movie/{created.Id}", created);
    }
    catch (ArgumentException error)
    {
        return Results.BadRequest(error);
    }
});

app.MapPut("/json/movie/{id}", ([FromServices] MovieRepositoryJson repository, [FromRoute(Name = "id")] int id, [FromBody] Movie movie) =>
{
    try
    {
        return Results.Ok(repository.Update(id, movie));
    }
    catch (Exception error)
    {
        Console.WriteLine(error.Message);
        return Results.NotFound();
    }
});

app.MapDelete("/json/movie/{id}", ([FromServices] MovieRepositoryJson repository, [FromRoute] int id) =>
{
    try
    {
        repository.Delete(id);
        return Results.NoContent();
    }
    catch (Exception error)
    {
        Console.WriteLine(error.Message);
        return Results.NotFound();
    }
});

app.MapGet("/postgres/movie", ([FromServices] MovieRepositoryPostgresSql postgres, [FromQuery] string? name) => {
    if (string.IsNullOrEmpty(name))
        return Results.Ok(postgres.SearchAll());

    return Results.Ok(postgres.SearchByName(name));
});

app.MapGet("/postgres/movie/{id:int}", ([FromServices] MovieRepositoryPostgresSql postgres, [FromRoute(Name = "id")] int id) => {
    var movie = postgres.SearchById(id);

    if (movie == null)
        return Results.NotFound();
    
    return Results.Ok(movie);
});



app.Run();