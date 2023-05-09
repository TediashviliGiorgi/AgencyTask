using UserManagementSystem.Repositories;

namespace UserManagementSystem.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        Task SaveChangesAsync();
    }
}
