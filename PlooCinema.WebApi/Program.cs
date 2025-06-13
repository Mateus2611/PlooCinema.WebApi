using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using PlooCinema.Core.Repositories;
using PlooCinema.Core.Services.Interfaces;
using PlooCinema.Core.Services;
using PlooCinema.Infrastructure.Data;
using PlooCinema.Infrastructure.Data.Repositories;
using PlooCinema.Core.Responses;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .EnableSensitiveDataLogging()
        .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
});

builder.Services
    .AddIdentityApiEndpoints<AppUser>()
    .AddRoles<AppRole>()
    .AddEntityFrameworkStores<DataContext>();

builder.Services.AddAuthorization();

builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

builder.Services.AddScoped<IMovieRepository, EFMovieRepository>();

builder.Services.AddScoped<IGenreRepository, EFGenreRepository>();

builder.Services.AddScoped<IRoomRepository, EFRoomRepository>();

builder.Services.AddScoped<ISessionRepository, EFSessionRepository>();

builder.Services.AddScoped<IMovieService, MovieServices>();

builder.Services.AddScoped<IGenreService, GenreService>();

builder.Services.AddScoped<IRoomServices, RoomServices>();

builder.Services.AddScoped<ISessionService, SessionService>();

builder.Services.AddAutoMapper(typeof(AutoMapperResponses));

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

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("auth").MapIdentityApi<AppUser>().WithTags("Authorization");

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

app.MapGroup("auth").MapPost("/logout",
    async (SignInManager<AppUser> signInManager, [FromBody] object empty) =>
    {
        await signInManager.SignOutAsync();
        return Results.NoContent();
    }).WithTags("Authorization");

app.MapControllers();

app.Run();