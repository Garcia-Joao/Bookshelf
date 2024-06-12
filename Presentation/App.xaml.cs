using Bookshelf.Domain.DataModels;
using Bookshelf.Domain.Mappers;
using Bookshelf.Domain.Services;
using Bookshelf.Helpers;
using Bookshelf.Infrastructure.Controllers;
using Bookshelf.Infrastructure.Database;
using Bookshelf.Infrastructure.Domain.Controllers;
using Bookshelf.Infrastructure.Entities;
using Bookshelf.Presentation.View;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace Bookshelf.Presentation {
    public partial class App : Application {
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

            IHost host = builder.Build();

            base.OnStartup(e);

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
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
    }

}
