using DalilAcademy.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Service
{
    public interface IPaymentService
    {
        Task<PayResponseDto> PayAsync(int enrollmentId);
        Task<EnrollmentResponseDto> CaptureAsync(int enrollmentId);
    }
}
