using PlooCinema.WebApi.Repositories;
using PlooCinema.WebApi.Model;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{
    options.SwaggerDoc("v1", new OpenApiInfo {
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

app.MapGet("/json/movie", () => repository.SearchAll());
app.MapGet("/json/movie/search", (string name) => repository.SearchByName(name));
app.MapGet("/json/movie/{id}", (int id) => repository.SearchById(id));
app.MapPost("/json/movie", (Movie movie) => repository.Create(movie));
app.MapPut("/json/movie/{id}", (int id, Movie movie) => repository.Update(id, movie));
app.MapDelete("/json/movie/{id}", (int id) => repository.Delete(id));

app.Run();