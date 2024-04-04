using Exam.Enums;

namespace Exam.Models
{
    public class User
    {
        public int Id { get; set; }
        public RoleType roleType { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
