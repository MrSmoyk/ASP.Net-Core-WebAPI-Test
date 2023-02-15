using DAL.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class DogRepository : BaseRepository<Dog>, IDogRepository
    {
        public DogRepository(ApplicationDbContext applicationContext) : base(applicationContext)
        {
        }

        public override IQueryable<Dog> GetAll()
        {
            return ApplicationContext.Set<Dog>()
                .AsNoTracking();
        }

        public override IQueryable<Dog> GetByState(Expression<Func<Dog, bool>> expression)
        {
            return ApplicationContext.Set<Dog>()
                .Where(expression)
                .AsNoTracking();
        }
    }
}
