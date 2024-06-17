using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Infrastructure.Entities {
    public abstract class Entity {
        [Key]
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
