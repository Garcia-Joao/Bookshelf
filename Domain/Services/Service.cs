using Bookshelf.Domain.DataModels;
using Bookshelf.Domain.Mappers;
using Bookshelf.Infrastructure.Domain.Controllers;
using Bookshelf.Infrastructure.Entities;

namespace Bookshelf.Domain.Services {
    public abstract class Service<E, D> where E: Entity 
                                        where D: Datamodel
    {
        protected readonly Controller<E> controller;
        protected readonly Mapper<E, D> mapper;

        protected Service(Controller<E> controller, Mapper<E, D> mapper) {
            this.controller = controller;
            this.mapper = mapper;
        }

        protected void Add(D datamodelToAdd) {
            controller.Add(mapper.Map(datamodelToAdd));
        }

        protected E GetEntityById(Guid id) {
            return controller.GetById(id)!;
        }

        public D GetDatamodelById(Guid id) {
            return mapper.Map(GetEntityById(id));
        }
    }
}
