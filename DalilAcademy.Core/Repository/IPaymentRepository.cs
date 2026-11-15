using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Repository
{
    public interface IPaymentRepository
    {
        public interface IPaymentRepository
        {
            Task<Payment> CreateAsync(Payment payment);
            Task<Payment?> GetLatestCreatedAsync(int enrollmentId);
            Task UpdateAsync(Payment payment);
        }
    }
}
