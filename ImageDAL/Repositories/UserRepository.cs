using ImageDAL.Models;
using ImageDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ImageDAL.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ImageDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User> GetAsync(int id)
    {
        return await GetAll().FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<User> GetAsyncByEmail(string email)
    {
        return await GetAll().FirstOrDefaultAsync(user => user.Email.Equals(email));
    }
}