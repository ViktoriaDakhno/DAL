using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LibraryApp.DAL
{
    public class LibraryContextFactory : IDesignTimeDbContextFactory<LibraryContext>
    {
        public LibraryContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=LibraryDb;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True");

            return new LibraryContext(optionsBuilder.Options);
        }
    }
}
