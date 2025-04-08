using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DAL
{
    public class LibraryContext : DbContext
    {
        // DbSet-и для сутностей
        public DbSet<Content> Contents { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=LibraryDb;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Content>()
                .HasOne(c => c.Metadata)
                .WithOne(m => m.Content)
                .HasForeignKey<Metadata>(m => m.ContentId);

            modelBuilder.Entity<Content>()
                .HasMany(c => c.Tags)
                .WithMany(t => t.Contents);

            base.OnModelCreating(modelBuilder);
        }

    }
}
