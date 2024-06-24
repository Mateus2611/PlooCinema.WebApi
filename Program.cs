using PlooCinema.WebApi.Repositories;
using PlooCinema.WebApi.Model;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/json/movie", () => IMovieRepository.Create(movie));

app.Run();