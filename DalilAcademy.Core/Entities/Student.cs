using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }
        public List<Enrollment> Enrollments { get; set; } = [];

    }
}
