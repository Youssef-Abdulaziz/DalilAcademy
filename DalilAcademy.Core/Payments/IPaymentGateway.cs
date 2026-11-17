using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Payments
{
    public record GatewayOrder(string OrderId, string ApprovalUrl);

    public interface IPaymentGateway
    {
        Task<GatewayOrder> CreateOrderAsync(decimal amountUsd);
        Task<bool> CaptureOrderAsync(string orderId);   // true only when PayPal answers COMPLETED
    }
}
