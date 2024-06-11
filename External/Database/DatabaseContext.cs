using Bookshelf.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookshelf.External.Database {
    public class DatabaseContext : DbContext {
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<GenreEntity> Genres { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {
        }
    }
}
