using DalilAcademy.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int EnrollmentId { get; set; }
        public Enrollment? Enrollment { get; set; }
        public decimal Amount { get; set; }                  // in USD
        public string Currency { get; set; } = "USD";
        public string PayPalOrderId { get; set; } = "";
        public PaymentStatus Status { get; set; } = PaymentStatus.Created;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PaidAt { get; set; }
    }
}
