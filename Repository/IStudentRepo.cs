using PersonalAccount.Models.Students;

namespace PersonalAccount.Repository
{
    public interface IStudentRepo<T> where T : StudentModel
    {
        public Task<T?> GetByEmailAsync(string email);
        public Task<T?> GetByIdAsync(int id);
        public Task UpdateByIdAsync(int id, StudentModel student);
        public Task UpdatePasswordHashAsync(int id, string passwordHash);
    }
}
