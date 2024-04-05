using Exam.Interfaces;
using Exam.Models;

namespace Exam.Services
{
    public class UserService : IUserService
    {
        private readonly ExamDbContext context;
        private readonly JwtService jwtService;

        public UserService(ExamDbContext context, JwtService jwtService)
        {
            this.context = context;
            this.jwtService = jwtService;
        }
        public User GetById(int id)
        {
            return context.Users.Find(id);
        }
        public IEnumerable<User> GetAll()
        {
            return context.Users.ToList();
        }
        public void Add(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
        public void Update(int id, User user)
        {
            var prevUser = GetById(id);

            if (prevUser != null)
            {
                prevUser.Name = user.Name;
                prevUser.Email = user.Email;
                prevUser.Password = user.Password;
                prevUser.roleType = user.roleType;

                context.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            var user = GetById(id);

            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }
        public User GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(x => x.Email == email);
        }

        public (string Token, User User) AuthenticateWithDetails(string email, string password)
        {
            var user = context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);

            if (user == null)
                return (null, null);

            var token = jwtService.GenerateToken(user);
            user.Token = token;
            context.SaveChanges();

            return (token, user);
        }      
       
    }
}
