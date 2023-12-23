namespace ImageDAL.Repositories.Interrfaces;

public interface IRepository<EntType>
{
    IQueryable<EntType> GetAll();
    Task<EntType> CreateAsync(EntType entity);
    Task<IEnumerable<EntType>> CreateAsync(IEnumerable<EntType> entities);
    Task<int> UpdateAsync(EntType entity);
    Task<int> UpdateAsync(IEnumerable<EntType> entities);
    Task<EntType> DeleteAsync(EntType entity);
    Task<int> DeleteAsync(IEnumerable<EntType> entities);
}