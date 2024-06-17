using Bookshelf.Domain.DataModels;
using Bookshelf.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Presentation.Models {
    public class AuthorsModel {

        AuthorService authorService;
        public AuthorsModel(AuthorService authorService) { 
            this.authorService = authorService;
        }

        public Author GetAuthorByName(string name) {
            return authorService.GetAuthorByName(name);
        }
    }
}
