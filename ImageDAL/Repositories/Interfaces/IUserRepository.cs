using ImageDAL.Models;
using ImageDAL.Repositories.Interrfaces;

namespace ImageDAL.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetAsync(int id);
    Task<User> GetAsyncByEmail(string email);
}