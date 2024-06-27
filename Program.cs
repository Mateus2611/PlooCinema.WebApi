using PlooCinema.WebApi.Repositories;
using PlooCinema.WebApi.Model;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<MovieRepositoryPostgresSql>(ServiceProvider => {
    var connString = ServiceProvider.GetRequiredService<IConfiguration>()
                                    .GetConnectionString("DefaultConnection");
    return new MovieRepositoryPostgresSql(connString);
});

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

app.MapGet("/postgres/movie", ([FromServices]MovieRepositoryPostgresSql postgres, [FromQuery(Name = "name")] string? name) =>
{
    if (String.IsNullOrEmpty(name))
        return Results.Ok(postgres.SearchAll());
    
    return Results.Ok(postgres.SearchByName(name));
});
app.MapGet("/postgres/movie/{id:int}", ([FromServices]MovieRepositoryPostgresSql postgres, [FromRoute(Name = "id")] int id) => Results.Ok(postgres.SearchById(id)));
app.MapPost("/postgres/movie", ([FromServices]MovieRepositoryPostgresSql postgres, [FromBody] Movie movie) => postgres.Create(movie));
app.MapPut("/postgres/movie/{id:int}", ([FromServices]MovieRepositoryPostgresSql postgres, [FromRoute(Name = "id")]int id,[FromBody] Movie movie) => postgres.Update(id, movie));
app.MapDelete("/postgres/movie/{id:int}", ([FromServices]MovieRepositoryPostgresSql postgres, [FromRoute(Name = "id")] int id) => postgres.Delete(id));

app.Run();