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
        public static async Task Initialize(ApplicationContext context, IServiceProvider services)
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
                    await authRepo.Register(user, "654321");
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

            #region seed leaderboards table
            if (unitOfWork.Repository<Leaderboard>().FindAll().Any())
            {
                logger.LogInformation("Leaderboards table was already seeded");
            }
            else
            {
                var races = unitOfWork.Repository<Race>().FindAll().OrderBy(x => x.Id);
                var raceMinId = races.FirstOrDefault().Id;
                var raceMaxId = races.LastOrDefault().Id;

                var users = unitOfWork.Repository<User>().FindAll().OrderBy(x => x.Id);
                var userMinId = users.FirstOrDefault().Id;
                var userMaxId = users.LastOrDefault().Id;

                var countries = unitOfWork.Repository<Country>().FindAll().OrderBy(x => x.Id);
                var countryMinId = countries.FirstOrDefault().Id;
                var countryMaxId = countries.LastOrDefault().Id;

                var vehicles = unitOfWork.Repository<Vehicle>().FindAll().OrderBy(x => x.Id);
                var vehicleMinId = vehicles.FirstOrDefault().Id;
                var vehiclesMaxId = vehicles.LastOrDefault().Id;

                var platforms = unitOfWork.Repository<Platform>().FindAll().OrderBy(x => x.Id);
                var platformMinId = platforms.FirstOrDefault().Id;
                var platformMaxId = platforms.LastOrDefault().Id;

                var random = new Random();
                
                for (int i = 0; i < 200; i++)
                {
                    while(true)
                    {
                        var userId = GetValidUserId(services, userMinId, userMaxId);
                        var raceId = GetValidRaceId(services, raceMinId, raceMaxId);

                        if (unitOfWork.Repository<Leaderboard>().FindAll().Any(x => x.UserId == userId && x.RaceId == raceId))
                        {
                            continue; // prevent duplicate entries
                        }

                        var countryId = GetValidCountryId(services, countryMinId, countryMaxId);
                        var vehicleId = GetValidVehicleId(services, vehicleMinId, vehiclesMaxId);
                        var platformId = GetValidPlatformId(services, platformMinId, platformMaxId);
                        var time = random.Next(180000, 600000); // a value between 3 and 10 minutes (in milliseconds)

                        var leaderboard = new Leaderboard
                        {
                            UserId = userId,
                            RaceId = raceId,
                            CountryId = countryId,
                            VehicleId = vehicleId,
                            PlatformId = platformId,
                            Time = time
                        };

                        unitOfWork.Repository<Leaderboard>().Create(leaderboard);
                        unitOfWork.SaveChanges();

                        break;
                    }
                }
            }
            #endregion

            logger.LogInformation("Finished seeding the database.");
        }

        private static string GetRandomString(int length) =>
            Path.GetRandomFileName().Replace(".", "").Substring(0, length).ToUpper();

        private static int GetValidUserId(IServiceProvider services, int minId, int maxId)
        {
            var unitOfWork = services.GetRequiredService<IUnitOfWork>();
            var random = new Random();
            int id = 0;

            while(true)
            {
                id = random.Next(minId, maxId);

                if (unitOfWork.Repository<User>().FindAll().Any(x => x.Id == id))
                {
                    break;
                }
            }

            return id;
        }

        private static int GetValidRaceId(IServiceProvider services, int minId, int maxId)
        {
            var unitOfWork = services.GetRequiredService<IUnitOfWork>();
            var random = new Random();
            int id = 0;

            while (true)
            {
                id = random.Next(minId, maxId);

                if (unitOfWork.Repository<Race>().FindAll().Any(x => x.Id == id))
                {
                    break;
                }
            }

            return id;
        }

        private static int GetValidCountryId(IServiceProvider services, int minId, int maxId)
        {
            var unitOfWork = services.GetRequiredService<IUnitOfWork>();
            var random = new Random();
            int id = 0;

            while (true)
            {
                id = random.Next(minId, maxId);

                if (unitOfWork.Repository<Country>().FindAll().Any(x => x.Id == id))
                {
                    break;
                }
            }

            return id;
        }

        private static int GetValidVehicleId(IServiceProvider services, int minId, int maxId)
        {
            var unitOfWork = services.GetRequiredService<IUnitOfWork>();
            var random = new Random();
            int id = 0;

            while (true)
            {
                id = random.Next(minId, maxId);

                if (unitOfWork.Repository<Vehicle>().FindAll().Any(x => x.Id == id))
                {
                    break;
                }
            }

            return id;
        }

        private static int GetValidPlatformId(IServiceProvider services, int minId, int maxId)
        {
            var unitOfWork = services.GetRequiredService<IUnitOfWork>();
            var random = new Random();
            int id = 0;

            while (true)
            {
                id = random.Next(minId, maxId);

                if (unitOfWork.Repository<Platform>().FindAll().Any(x => x.Id == id))
                {
                    break;
                }
            }

            return id;
        }

    }
}
