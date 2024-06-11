using Bookshelf.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Infrastructure.Domain.Controllers {
    public abstract class Controller<T> : IController<T> where T : Entity {
        public abstract void Add(T entityToAdd);
        public abstract void Remove(T entityToRemove);
        public abstract void Update(T entityToUpdate);
    }
}
