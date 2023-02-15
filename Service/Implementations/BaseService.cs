using AutoMapper;
using DAL.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Domain.Helpers;
using Service.Interfaces;

namespace Service.Implementations
{
    public abstract class BaseService<TEntity, DTO> : IBaseService<TEntity, DTO>
        where TEntity : BaseEntity where DTO : BaseDTO
    {
        protected readonly IBaseRepository<TEntity> baseRepository;
        protected readonly ISortHelper<TEntity> sortHelper;
        protected readonly IMapper mapper;

        protected BaseService(IBaseRepository<TEntity> _baseRepository, ISortHelper<TEntity> _sortHelper, IMapper _mapper)
        {
            baseRepository = _baseRepository;
            sortHelper = _sortHelper;
            mapper = _mapper;
        }

        public async Task<IQueryable<TEntity>> GetAllEntitysAsync()
        {
            return baseRepository.GetAll();
        }

        public async Task<DTO> GetByIdAsync(int id)
        {
            var entitys = await baseRepository.GetByIdAsync(id);
            return mapper.Map<DTO>(entitys);
        }

        public async Task<DTO> CreateAsync(DTO dto)
        {
            var entity = mapper.Map<TEntity>(dto);
            var entityToCreate = await baseRepository.CreateAsync(entity);
            return mapper.Map<DTO>(entityToCreate);
        }

        public async Task<DTO> UpdateAsync(DTO dto)
        {
            var entity = mapper.Map<TEntity>(dto);
            var updateEntity = await baseRepository.UpdateAsync(entity);
            return mapper.Map<DTO>(updateEntity);
        }

        public async Task DeleteAsync(int id)
        {
            await baseRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await baseRepository.ExistsAsync(id);
        }
    }
}
