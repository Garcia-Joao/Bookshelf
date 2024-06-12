using Bookshelf.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookshelf.Infrastructure.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<GenreEntity> Genres { get; set; }  

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<T> GetDbSet<T>() where T : Entity {
            var property = typeof(DatabaseContext).GetProperties()
                .FirstOrDefault(p => p.PropertyType == typeof(DbSet<T>));

            if (property != null) {
                return property.GetValue(this) as DbSet<T>;
            }

            throw new InvalidOperationException($"DbSet of type {typeof(T).Name} not found in DatabaseContext.");
        }
    }
}
