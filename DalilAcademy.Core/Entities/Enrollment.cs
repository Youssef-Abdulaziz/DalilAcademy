using DalilAcademy.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public DateTime EnrolledAt { get; set; }

        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Pending;
        public List<Payment> Payments { get; set; } = [];


    }
}
