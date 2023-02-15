using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DbInitializer
    {
        public static async void Initialize(ApplicationDbContext dbContext)
        {
            await dbContext.Database.EnsureCreatedAsync();
            if (!dbContext.Dogs.Any())
            {
                var dogs = new Dog[]
                {
                    new Dog { Name = "Neo", Color = "red & amber", TailLength = 22, Weight = 32},
                    new Dog { Name = "Jessy", Color = "black & white", TailLength = 7, Weight = 14}
                };

                dbContext.Dogs.AddRangeAsync(dogs);

                dbContext.SaveChangesAsync();
            }

            foreach (var entity in dbContext.ChangeTracker.Entries())
            {
                entity.State = EntityState.Detached;
            }

        }
    }
}
