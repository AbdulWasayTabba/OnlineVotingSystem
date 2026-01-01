using practice.DTOs;
using practice.Models;

namespace practice.Repository.Interface
{
    public interface IUserRepository
    {

        public Task<User?> FindUser(string email);

        public Task<bool> checkEmailPreExisting(string email);
    }
}
