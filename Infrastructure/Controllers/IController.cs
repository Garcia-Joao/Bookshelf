using Bookshelf.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Infrastructure.Domain.Controllers {
    public interface IController<T> where T : Entity {
        public void Add(T entityToAdd);
        public void Remove(T entityToRemove);
        public void Update(T entityToUpdate);
    }
}