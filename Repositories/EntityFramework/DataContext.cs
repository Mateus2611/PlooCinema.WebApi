using Microsoft.EntityFrameworkCore;
using PlooCinema.WebApi.Model;

namespace PlooCinema.WebApi.Repositories.EntityFramework
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>().ToTable("movie");
            modelBuilder.Entity<Movie>().HasKey(movie => new { movie.Id });
            modelBuilder.Entity<Movie>().Property(movie => movie.Id).HasColumnName("id");
            modelBuilder.Entity<Movie>().Property(movie => movie.Name).HasColumnName("name");
            modelBuilder.Entity<Movie>().Property(movie => movie.Duration).HasColumnName("duration");
            modelBuilder.Entity<Movie>().Property(movie => movie.Release).HasColumnName("release");
            modelBuilder.Entity<Movie>().Property(movie => movie.Description).HasColumnName("description");
            modelBuilder.Entity<Movie>()
                .HasMany(movie => movie.Genres)
                .WithMany(genre => genre.Movies)
                .UsingEntity(j => j.ToTable("movie_genre"));
        }
    }
}
