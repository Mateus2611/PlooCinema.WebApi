using Microsoft.EntityFrameworkCore;
using PlooCinema.WebApi.Model;
using PlooCinema.WebApi.Models;

namespace PlooCinema.WebApi.Repositories.EntityFramework
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public required DbSet<Movie> Movies { get; set; }
        public required DbSet<Genre> Genres { get; set; }
        public required DbSet<Room> Rooms { get; set; }
        public required DbSet<Session> Sessions { get; set; }
    }
}
