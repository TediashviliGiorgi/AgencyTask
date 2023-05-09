using UserManagementSystem.DB.Entities;
using UserManagementSystem.Models.Requests;
using UserManagementSystem.UnitOfWork;

namespace UserManagementSystem.Services
{
    public interface IUserService
    {
        public Task<UserEntity> CreateUser(CreateUserRequest request);
        public Task<UserEntity> UpdateUser(int userId, UpdateUserRequest request);
        public Task DeleteUser(int userId);
        public Task<UserEntity> GetUserById(int userId);
    }
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserEntity> CreateUser(CreateUserRequest request)
        {
            if (await _unitOfWork.UserRepository.ExistsByEmailAsync(request.Email))
            {
                throw new ArgumentException("A user with the specified email already exists.");
            }

            var newUser = new UserEntity
            {
                Email = request.Email,
                Password = request.Password,
                RoleId = request.RoleId
            };

            return await _unitOfWork.UserRepository.AddAsync(newUser);
        }

        public async Task<UserEntity> UpdateUser(int userId, UpdateUserRequest request)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            user.Email = request.Email ?? user.Email;
            user.Password = request.Password != null ? request.Password : user.Password; // Note: It's recommended to hash the password before storing it.
            user.RoleId = request.RoleId != 0 ? request.RoleId : user.RoleId;

            return await _unitOfWork.UserRepository.UpdateAsync(user);
        }

        public async Task DeleteUser(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            await _unitOfWork.UserRepository.DeleteAsync(user);
        }

        public async Task<UserEntity> GetUserById(int userId)
        {
            return await _unitOfWork.UserRepository.GetByIdAsync(userId);
        }
    }
}
