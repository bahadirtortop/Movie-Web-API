using Microsoft.EntityFrameworkCore;

namespace Movie.Domain
{
    public class MovieContext:DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) 
            : base(options)
        {

        }

        public virtual DbSet<Model.Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.Movie>(entity =>
            {
                entity.ToTable("Movie", "Movie");

                entity.Property(e => e.Id);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Year)
                    .IsRequired();

                entity.Property(e => e.Released)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Runtime)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Genre)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Director)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Writer)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Actors)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Poster)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ImdbRating)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
