using Bookshelf.Infrastructure.Database;
using Bookshelf.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
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
                _context.Remove(entity);
                _context.SaveChanges();
            }
        }

        public virtual void Update(T entityToUpdate) {

            var dbSet = _context.GetDbSet<T>();
            var entity = dbSet.Find(entityToUpdate.Id);

            if (entity != null) {
                _context.Entry(entity).CurrentValues.SetValues(entityToUpdate);
                entity.Updated = DateTime.UtcNow;
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            } else {
                throw new InvalidOperationException("Entity not found in the database.");
            }
        }

        public virtual List<T> GetAll() {
            return _context.GetDbSet<T>().ToList();
        }
        public virtual T GetById(Guid id) {

            T entity;
            try {
                entity = _context.GetDbSet<T>().First(e => e.Id == id);
            } catch (Exception ex) {
                entity = null;
            }
            return entity;
        }
    }
}
