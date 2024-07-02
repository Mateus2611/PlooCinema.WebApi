using PlooCinema.WebApi.Repositories;
using PlooCinema.WebApi.Model;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

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

var repository = new MovieRepositoryJson();

app.MapGet("/json/movie", ([FromBody] string name) => {
    if (string.IsNullOrEmpty(name))
        Results.Ok(repository.SearchAll());

    Results.Ok(repository.SearchByName(name));
});
app.MapGet("/json/movie/{id}", ([FromRoute(Name = "id")] int id) =>
{
    var movie = repository.SearchById(id);

    if (movie == null)
        Results.NotFound();

    Results.Ok();
});
app.MapPost("/json/movie", ([FromBody] Movie movie) =>
{
    var created = repository.Create(movie);
    Results.Created($"/json/movie/{created.Id}", created);
});
app.MapPut("/json/movie/{id}", ([FromRoute(Name = "id")] int id, [FromBody] Movie movie) => 
{
    try
    {
        Results.Ok(repository.Update(id, movie));
    } catch (Exception error)
    {
        Console.WriteLine(error.Message);
        Results.NotFound();
    }
});
app.MapDelete("/json/movie/{id}", ([FromRoute] int id) => {
    try
    {
        repository.Delete(id);
        Results.NoContent(); 
    } catch (Exception error)
    {
        Console.WriteLine(error.Message);
        Results.NotFound();
    }
});

app.Run();