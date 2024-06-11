using Bookshelf.External.Database;
using Bookshelf.Helpers;
using Bookshelf.Presentation.View;
using Microsoft.Data.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.IO;
using System.Windows;

namespace Bookshelf.Presentation {
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {

            HostApplicationBuilder builder = Host.CreateApplicationBuilder();

            InjectDatabaseContext(builder.Services);

            builder.Services.AddDbContext<DatabaseContext>();

            builder.Build();

            base.OnStartup(e);

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void InjectDatabaseContext(IServiceCollection services) {

            string connectionString = JsonHelper.GetConfigurationData("ConnectionStrings", "SQLConnectionString");

            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new DatabaseContext(optionsBuilder.Options))
            {
                if (!context.Database.CanConnect())
                {
                    //Database creation :)
                    context.Database.EnsureCreated();
                }
            }
        }
    }

}
