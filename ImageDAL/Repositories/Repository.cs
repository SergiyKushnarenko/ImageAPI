using ImageDAL.Repositories.Interrfaces;

namespace ImageDAL.Repositories;

public abstract class Repository<EntType> : IRepository<EntType> where EntType : class
{
    private readonly ImageDbContext _context;

    protected Repository(ImageDbContext context)
    {
        _context = context;
    }

    public IQueryable<EntType> GetAll()
    {
        return _context.Set<EntType>().AsQueryable();
    }

    public async Task<EntType> CreateAsync(EntType entity)
    {
        var result = _context.Add(entity);
        await _context.SaveChangesAsync(); 
        return result.Entity; 
    }

    public async Task<IEnumerable<EntType>> CreateAsync(IEnumerable<EntType> entities)
    {
        _context.AddRange(entities);
        await _context.SaveChangesAsync();
        return entities;
    } 

    public Task<int> UpdateAsync(EntType entity)
    {
        _context.Update(entity);
        return _context.SaveChangesAsync();
    }

    public Task<int> UpdateAsync(IEnumerable<EntType> entities)
    {
        _context.UpdateRange(entities);
        return _context.SaveChangesAsync();
    }

    public async Task<EntType> DeleteAsync(EntType entity)
    {
        var result = _context.Remove(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public Task<int> DeleteAsync(IEnumerable<EntType> entities)
    {
        _context.RemoveRange(entities);
        return _context.SaveChangesAsync();
    }
}