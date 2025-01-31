using PlooCinema.WebApi.Repositories;
using Microsoft.OpenApi.Models;
using PlooCinema.WebApi.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using PlooCinema.WebApi.Services.Interfaces;
using PlooCinema.WebApi.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseLazyLoadingProxies()
        .EnableSensitiveDataLogging()
        .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
});

builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

builder.Services.AddScoped<IMovieRepository, EFMovieRepository>();

builder.Services.AddScoped<IGenreRepository, EFGenreRepository>();

builder.Services.AddScoped<IMovieService, MovieServices>();

builder.Services.AddScoped<IGenreService, GenreService>();

builder.Services.AddScoped<IRoomRepository, EFRoomRepository>();

builder.Services.AddScoped<IRoomServices, RoomServices>();

builder.Services.AddScoped<ISessionRepository, EFSessionRepository>();

builder.Services.AddScoped<ISessionService, SessionService>();

builder.Services.AddAutoMapper(typeof(Program));

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