using Bookshelf.Infrastructure.Database;
using Bookshelf.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Bookshelf.Infrastructure.Domain.Controllers {
    public abstract class Controller<T> : IController<T> where T : Entity {

        protected readonly DatabaseContext _context;

        protected Controller(DatabaseContext context) {
            _context = context;
        }

        public virtual void Add(T entityToAdd) {
            entityToAdd.Created = DateTime.UtcNow;
            entityToAdd.Updated = DateTime.UtcNow;
            _context.GetDbSet<T>().Add(entityToAdd);
            _context.SaveChanges();
        }

        public virtual void Remove(T entityToRemove) {
            var entity = _context.GetDbSet<T>().Find(entityToRemove.Id);
            if (entity != null) {
                entity.Deleted = DateTime.UtcNow;
                entity.IsDeleted = true;
                _context.SaveChanges();
            }
        }

        public virtual void Update(T entityToUpdate) {
            var entity = _context.GetDbSet<T>().Find(entityToUpdate.Id);
            if (entity != null) {
                entity.Updated = DateTime.UtcNow;
                _context.GetDbSet<T>().Update(entity);
                _context.SaveChanges();
            }
        }

        public virtual List<T> GetAll() {
            return _context.Set<T>().ToList();
        }
        public virtual T GetById(Guid id) {
            return _context.GetDbSet<T>().Find(id)!;
        }
    }
}
