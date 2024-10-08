using PlooCinema.WebApi.Repositories;
using PlooCinema.WebApi.Model;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using PlooCinema.WebApi.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PlooCinema.WebApi.Models;
using PlooCinema.WebApi.Services.Interfaces;
using PlooCinema.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseLazyLoadingProxies()
        .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
});
builder.Services.AddScoped<IMovieRepository, EFMovieRepository>();
builder.Services.AddScoped<IGenreRepository, EFGenreRepository>();
builder.Services.AddScoped<IMovieService, MovieServices>();

// builder.Services.AddScoped<IMovieRepository, MovieRepositoryPostgreSql>(ServiceProvider =>
// {
//     var connString = ServiceProvider
//         .GetRequiredService<IConfiguration>()
//         .GetConnectionString("DefaultConnection");

//     return new MovieRepositoryPostgreSql(connString);
// });

// builder.Services.AddScoped<IGenreRepository, GenreRepositoryPostgreSql>(ServiceProvider =>
// {
//     var connString = ServiceProvider
//         .GetRequiredService<IConfiguration>()
//         .GetConnectionString("DefaultConnection");

//     return new GenreRepositoryPostgreSql(connString);
// });

builder.Services.AddControllers();
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

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger");
        return;
    }
    await next();
});

app.MapControllers();

app.Run();