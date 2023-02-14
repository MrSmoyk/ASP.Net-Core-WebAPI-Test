using DAL.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
