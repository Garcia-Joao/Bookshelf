using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Infrastructure.Entities {
    public class BookEntity : Entity {
        public string Title { get; set; }
        public string? Description { get; set; }
        public Guid Author_Id { get; set; }
        public Guid Genre_Id { get; set; }
    }
}
