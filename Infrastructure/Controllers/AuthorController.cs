using Bookshelf.Infrastructure.Domain.Controllers;
using Bookshelf.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Infrastructure.Controllers {
    public class AuthorController : Controller<AuthorEntity> {
        public override void Add(AuthorEntity entityToAdd) {

        }

        public override void Remove(AuthorEntity entityToRemove) {
            throw new NotImplementedException();
        }

        public override void Update(AuthorEntity entityToUpdate) {
            throw new NotImplementedException();
        }
    }
}
