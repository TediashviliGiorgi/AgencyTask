using UserManagementSystem.DB.Entities;
using UserManagementSystem.UnitOfWork;

namespace UserManagementSystem.Services
{
    public interface IUserRoleService
    {
        Task AssignRoleToUser(int userId, int roleId);
        Task<List<UserEntity>> GetUsersByRole(int roleId);
    }

    public class UserRoleService : IUserRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserRoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AssignRoleToUser(int userId, int roleId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            var role = await _unitOfWork.UserRepository.GetByIdAsync(roleId);

            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            if (role == null)
            {
                throw new ArgumentException("Role not found.");
            }

            user.RoleId = roleId;
            await _unitOfWork.UserRepository.UpdateAsync(user);
        }

        public async Task<List<UserEntity>> GetUsersByRole(int roleId)
        {
            return await _unitOfWork.UserRoleRepository.GetByRoleAsync(roleId);
        }
    }
}
