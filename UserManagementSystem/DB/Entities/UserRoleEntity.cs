namespace UserManagementSystem.DB.Entities
{
    public class UserRoleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserEntity> Users { get; set; }
    }
}
