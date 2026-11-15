using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.DTOs
{
    public record ConvertPriceRequestDto(decimal Amount, string FromCurrency, string ToCurrency);
    public record ConvertPriceResponseDto(decimal OriginalAmount, string FromCurrency, decimal ConvertedAmount, string ToCurrency, decimal ExchangeRate);
}
