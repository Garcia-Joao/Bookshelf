using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Domain.DataModels {
    public class Book : Datamodel {
        public string title { get; set; }
        public string description { get; set; }
        public Author? author { get; set; }
        public Genre? genre { get; set; }
    }
}
