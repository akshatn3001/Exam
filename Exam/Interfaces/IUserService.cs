using Exam.Models;

namespace Exam.Interfaces
{
    public interface IUserService
    {
        User GetById(int id);
        IEnumerable<User> GetAll();
        void Add(User user);
        void Update(int id, User user);
        void Delete(int id);
        User GetUserByEmail(string email);
        (string Token, User User) AuthenticateWithDetails(string email, string password);

    }
}
