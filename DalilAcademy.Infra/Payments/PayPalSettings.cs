using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Infra.Payments
{
    public  class PayPalSettings
    {
        public string BaseUrl { get; set; } = "";
        public string ClientId { get; set; } = "";
        public string ClientSecret { get; set; } = "";
        public string ReturnUrl { get; set; } = "";
        public decimal JodToUsdRate { get; set; }
    }
}
