using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Domain.DataModels {
    public abstract class Datamodel {
        public Guid Id {
            init; get;
        }
    }
}
