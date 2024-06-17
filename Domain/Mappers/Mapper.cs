using Bookshelf.Domain.DataModels;
using Bookshelf.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Domain.Mappers {
    public abstract class Mapper<E, D> : IMapper<E, D> where E : Entity
                                                      where D : Datamodel {
        public abstract D Map(E entity);
        public abstract E Map(D dataModel);

        public List<E> Map(List<D> dataModels) {
            List<E> returnList = new List<E>();
            foreach (D dataModel in dataModels) { 
                returnList.Add(Map(dataModel));
            }
            return returnList;
        }

        public List<D> Map(List<E> entities) {
            List<D> returnList = new List<D>();
            foreach (E entity in entities) {
                returnList.Add(Map(entity));
            }
            return returnList;
        }
    }
}
