using Microsoft.EntityFrameworkCore;
using UserManagementSystem.DB;
using UserManagementSystem.Repositories;

namespace UserManagementSystem.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            UserRepository = new UserRepository(_db);
            UserRoleRepository = new UserRoleRepository(_db);
        }

        public IUserRepository UserRepository { get; private set; }
        public IUserRoleRepository UserRoleRepository { get; private set; }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
