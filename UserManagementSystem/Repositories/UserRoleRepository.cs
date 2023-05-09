using Microsoft.EntityFrameworkCore;
using UserManagementSystem.DB;
using UserManagementSystem.DB.Entities;

namespace UserManagementSystem.Repositories
{ public interface IUserRoleRepository
    {
        Task<List<UserEntity>> GetByRoleAsync(int roleId);
    }

    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _db;

        public UserRoleRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<UserEntity>> GetByRoleAsync(int roleId)
        {
            return await _db.Users.Where(u => u.RoleId == roleId).ToListAsync();
        }
    }
}
