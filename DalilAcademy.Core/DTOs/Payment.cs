using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.DTOs
{
    public record PayResponseDto(int PaymentId, string OrderId, decimal Amount, string Currency, string ApprovalUrl);
}
