using Microsoft.EntityFrameworkCore;

namespace movieapp_backend.Features.Movies;

/// <summary>
/// Movie database context.
/// </summary>
public class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options)
    : base(options)
    {
    }

    /// <summary>
    /// Gets the movie entities.
    /// </summary>
    public DbSet<MovieEntity> Movies => Set<MovieEntity>();

    /// <summary>
    /// Gets the movie genre entities.
    /// </summary>
    public DbSet<GenreEntity> MovieGenres => Set<GenreEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieEntity>()
            .ToTable("Movies");
        modelBuilder.Entity<GenreEntity>()
            .ToTable("MovieGenres");

        modelBuilder.Entity<MovieEntity>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<GenreEntity>()
            .HasKey(x => new { x.Id, x.GenreId });

        modelBuilder.Entity<GenreEntity>()
            .HasOne(p => p.Movie)
            .WithMany(p => p.Genres)
            .HasForeignKey(p => p.Id);
    }
}
