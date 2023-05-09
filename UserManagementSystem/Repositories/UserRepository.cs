using Microsoft.EntityFrameworkCore;
using UserManagementSystem.DB;
using UserManagementSystem.DB.Entities;
using System.Linq;

namespace UserManagementSystem.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> AddAsync(UserEntity user);
        Task<UserEntity> UpdateAsync(UserEntity user);
        Task<UserEntity> GetByIdAsync(int id);
        Task DeleteAsync(UserEntity user);
        Task<bool> ExistsByEmailAsync(string email);
    }

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<UserEntity> AddAsync(UserEntity user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<UserEntity> UpdateAsync(UserEntity user)
        {
            _db.Entry(user).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<UserEntity> GetByIdAsync(int id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task DeleteAsync(UserEntity user)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }

        

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _db.Users.AnyAsync(u => u.Email == email);
        }
    }
}
