using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Repository
{
    public interface IEnrollmentRepository
    {
        Task<bool> ExistsAsync(int studentId, int courseId);
        Task<Enrollment> CreateAsync(Enrollment enrollment);
    }
}
