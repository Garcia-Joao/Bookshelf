using Bookshelf.Domain.DataModels;
using Bookshelf.Domain.Mappers;
using Bookshelf.Domain.Services;
using Bookshelf.Helpers;
using Bookshelf.Infrastructure.Controllers;
using Bookshelf.Infrastructure.Database;
using Bookshelf.Infrastructure.Domain.Controllers;
using Bookshelf.Infrastructure.Entities;
using Bookshelf.Presentation.Components;
using Bookshelf.Presentation.Models;
using Bookshelf.Presentation.ViewModels;
using Bookshelf.Presentation.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharpDX.Direct2D1.Effects;
using System.Windows;
using Telerik.Windows.Controls;

namespace Bookshelf.Presentation {
    public partial class App : Application {

        private IHost host;

        protected override void OnStartup(StartupEventArgs e) {

            HostApplicationBuilder builder = Host.CreateApplicationBuilder();

            string connectionString = JsonHelper.GetConfigurationData("ConnectionStrings", "SQLConnectionString");

            CreateDatabase(builder.Services, connectionString);

            //DatabaseCreation
            builder.Services.AddDbContext<DatabaseContext>(options =>
             options.UseSqlServer(connectionString),
             ServiceLifetime.Scoped);

            //Dependency Injection
            builder.Services.AddDbContext<DatabaseContext>();

            builder.Services.AddTransient<IMapper<AuthorEntity, Author>,AuthorMapper>();
            builder.Services.AddTransient<Controller<AuthorEntity>, AuthorController>();
            builder.Services.AddTransient<AuthorService>();

            builder.Services.AddTransient<IMapper<GenreEntity, Genre>,GenreMapper>();
            builder.Services.AddTransient<Controller<GenreEntity>, GenreController>();
            builder.Services.AddTransient<GenreService>();

            builder.Services.AddTransient<IMapper<BookEntity, Book>,BookMapper>();
            builder.Services.AddTransient<Controller<BookEntity>, BookController>();
            builder.Services.AddTransient<BookService>();
            
            builder.Services.AddTransient(services => {
                var bookModel = services.GetRequiredService<BooksModel>();
                return new BooksViewModel(services ,bookModel);
            });

            builder.Services.AddTransient(services => new BooksView() {
                DataContext = services.GetRequiredService<BooksViewModel>()
            });

            builder.Services.AddTransient(services => { 
                var authorService = services.GetRequiredService<AuthorService>(); 
                var genreService = services.GetRequiredService<GenreService>();
                var bookService = services.GetRequiredService<BookService>();
                return new MainModel(authorService, genreService, bookService); 
            });

            builder.Services.AddTransient(services => {
                var model = services.GetRequiredService<MainModel>();
                return new MainViewModel(model, services);
            });

            builder.Services.AddTransient(services => new MainWindow() {
                DataContext = services.GetRequiredService<MainViewModel>()
            });

            builder.Services.AddTransient<BooksModel>();

            builder.Services.AddTransient(services => new BooksView() {
                DataContext = services.GetRequiredService<BooksViewModel>()
            });

            builder.Services.AddTransient<AuthorsModel>();
            builder.Services.AddTransient<GenresModel>();

            host = builder.Build();

            //MockupInsetion
            //InsertMockup(host);

            base.OnStartup(e);

            MainWindow baseWindow = host.Services.GetService<MainWindow>();
            baseWindow.Show();
        }


        protected override void OnExit(ExitEventArgs e) {
            if (host != null) {
                host.Dispose();
            }
            base.OnExit(e);
        }


        private void CreateDatabase(IServiceCollection services, string connectionString) {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new DatabaseContext(optionsBuilder.Options)) {
                if (!context.Database.CanConnect()) {
                    //Database creation :)
                    context.Database.EnsureCreated();
                }
            }
        }

        private void InsertMockup(IHost host) {
            host.Services.GetService<GenreService>().InsertGenreMockups();
            host.Services.GetService<AuthorService>().InsertAuthorMockups();
            host.Services.GetService<BookService>().InsertBookMockup();
        }
    }

}
