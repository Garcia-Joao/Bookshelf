using Bookshelf.Domain.DataModels;
using Bookshelf.Infrastructure.Entities;

namespace Bookshelf.Domain.Mappers {
    public interface IMapper<E,D> where E: Entity 
                                  where D : Datamodel
    {
        public D Map(E entity);
        public E Map(D dataModel);
        public List<E> Map(List<D> dataModels);
        public List<D> Map(List<E> entities);
    }
}
