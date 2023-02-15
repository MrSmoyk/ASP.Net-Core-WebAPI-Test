namespace Service.Interfaces
{
    public interface IBaseService<TEntity, DTO>
    {
        public Task<IQueryable<TEntity>> GetAllEntitysAsync();
        public Task<DTO> GetByIdAsync(int id);
        public Task<DTO> CreateAsync(DTO dto);
        public Task<DTO> UpdateAsync(DTO dto);
        public Task DeleteAsync(int id);
        public Task<bool> ExistsAsync(int id);
    }
}
