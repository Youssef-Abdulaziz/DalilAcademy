using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = Roles.Student;
    }
    public static class Roles 
    {
        public const string Admin = "Admin", Instructor = "Instructor", Student = "Student"; 
    }
}
