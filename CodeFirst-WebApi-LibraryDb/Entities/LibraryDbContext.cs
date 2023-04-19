using CodeFirst_WebApi_LibraryDb.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CodeFirst_WebApi_LibraryDb.Entities
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<BookType> BookTypes { get; set; }
        public DbSet<User> Users { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
