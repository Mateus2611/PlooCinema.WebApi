using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlooCinema.WebApi.Model;
using PlooCinema.WebApi.Models;

namespace PlooCinema.WebApi.Repositories.EntityFramework
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Session> Sessions { get; set; }
    }
}
