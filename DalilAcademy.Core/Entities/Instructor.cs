using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public List<Course> Courses { get; set; } = [];
    }
}
