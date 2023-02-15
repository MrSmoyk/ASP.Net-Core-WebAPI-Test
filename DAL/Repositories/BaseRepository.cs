using DAL.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext ApplicationContext;
        private bool disposing;

        public BaseRepository(ApplicationDbContext applicationContext)
        {
            ApplicationContext = applicationContext;
        }

        public abstract IQueryable<T> GetAll();
        public abstract IQueryable<T> GetByState(Expression<Func<T, bool>> expression);

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = GetByState(x => x.Id == id);

            if (!entity.Any())
            {
                throw new EntityNotFoundException($"Entity with id: {id} not found.");
            }

            return await entity.FirstOrDefaultAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            if (entity is null)
            {
                throw new SourceEntityNullException("Entity to set wasn't given.");
            }

            var entityToCreate = await ApplicationContext.Set<T>().AddAsync(entity);
            await SaveChangesAsync();

            return await GetByIdAsync(entityToCreate.Entity.Id);
        }
        public async Task<T> UpdateAsync(T entity)
        {
            if (entity is null)
            {
                throw new SourceEntityNullException("Entity to set wasn't given.");
            }
            if (!await ExistsAsync(entity.Id))
            {
                throw new EntityNotFoundException($"Entity with id: {entity.Id} not found.");
            }

            var entityToUpdate = ApplicationContext.Set<T>().Update(entity);
            await SaveChangesAsync();

            return await GetByIdAsync(entityToUpdate.Entity.Id);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            ApplicationContext.Set<T>().Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await ApplicationContext.Set<T>().AnyAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await ApplicationContext.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        private async Task DisposeAsync(bool _disposing)
        {
            if (!disposing)
            {
                if (_disposing)
                {
                    await ApplicationContext.DisposeAsync();
                }
            }

            disposing = true;
        }
    }
}
