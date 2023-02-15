namespace Domain.Helpers
{
    public interface ISortHelper<TEntity>
    {
        IQueryable<TEntity> ApplySort(IQueryable<TEntity> entities, string orderByQueryString);
    }
}
