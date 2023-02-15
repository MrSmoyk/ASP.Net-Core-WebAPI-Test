using DAL;
using Microsoft.EntityFrameworkCore;

namespace Tests.DalTests.FakeDataBase
{
    public class FakeDbContext
    {
        public static ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().EnableSensitiveDataLogging()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var _context = new ApplicationDbContext(options);
            _context.Database.EnsureDeletedAsync();
            DbInitializer.Initialize(_context);
            return _context;
        }


    }
}
