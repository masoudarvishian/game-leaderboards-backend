using CodemastersLeaderboards.Application.Services.DomainService;
using CodemastersLeaderboards.Domain.Models;
using CodemastersLeaderboards.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodemastersLeaderboards.API.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationContext context, IServiceProvider services)
        {
            // Get a logger
            var logger = services.GetRequiredService<ILogger<DbInitializer>>();

            // get unit of work
            var unitOfWork = services.GetRequiredService<IUnitOfWork>();

            // get auth repository
            var authRepo = services.GetRequiredService<IAuthRepository>();

            // Make sure the database is created
            // We already did this in the previous step
            context.Database.EnsureCreated();

            logger.LogInformation("Start seeding the database.");

            #region seed users table
            if (unitOfWork.Repository<User>().FindAll().Any())
            {
                logger.LogInformation("Users table was already seeded");
            }
            else
            {
                for (int i = 0; i < 100; i++)
                {
                    var user = new User { Username = $"user_{GetRandomString(8)}" };
                    authRepo.Register(user, "654321");
                }

                unitOfWork.SaveChanges();
            }
            #endregion

            #region seed countries table
            if (unitOfWork.Repository<Country>().FindAll().Any())
            {
                logger.LogInformation("Countries table was already seeded");
            }
            else
            {
                for (int i = 0; i < 50; i++)
                {
                    var country = new Country { Name = $"country_{GetRandomString(7)}" };
                    unitOfWork.Repository<Country>().Create(country);
                }

                unitOfWork.SaveChanges();
            }
            #endregion

            #region seed platforms table
            if (unitOfWork.Repository<Platform>().FindAll().Any())
            {
                logger.LogInformation("Platforms table was already seeded");
            }
            else
            {
                var platforms = new List<Platform>()
                {
                    new Platform{ Name = "PC" },
                    new Platform{ Name = "PS4" },
                    new Platform{ Name = "PS5" },
                    new Platform{ Name = "Xbox Serie X" },
                    new Platform{ Name = "Xbox One" },
                    new Platform{ Name = "Nintendo Switch" },
                };

                foreach (var item in platforms)
                {
                    unitOfWork.Repository<Platform>().Create(new Platform
                    {
                        Name = item.Name
                    });
                }

                unitOfWork.SaveChanges();
            }
            #endregion

            #region seed vehicles table
            if (unitOfWork.Repository<Vehicle>().FindAll().Any())
            {
                logger.LogInformation("Vehicles table was already seeded");
            }
            else
            {
                for (int i = 0; i < 100; i++)
                {
                    var vehicle = new Vehicle { Name = $"car_{GetRandomString(7)}" };
                    unitOfWork.Repository<Vehicle>().Create(vehicle);
                }

                unitOfWork.SaveChanges();
            }
            #endregion

            #region seed races table
            if (unitOfWork.Repository<Race>().FindAll().Any())
            {
                logger.LogInformation("Races table was already seeded");
            }
            else
            {
                Random random = new Random();

                for (int i = 0; i < 200; i++)
                {
                    var race = new Race { LapCount = random.Next(3, 8) };
                    unitOfWork.Repository<Race>().Create(race);
                }

                unitOfWork.SaveChanges();
            }
            #endregion

            logger.LogInformation("Finished seeding the database.");
        }

        private static string GetRandomString(int length) =>
           Path.GetRandomFileName().Replace(".", "").Substring(0, length).ToUpper();
    }
}
