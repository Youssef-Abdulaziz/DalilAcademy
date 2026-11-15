using DalilAcademy.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Service
{
    public interface ICurrencyService
    {
        Task<ConvertPriceResponseDto> ConvertAsync(ConvertPriceRequestDto dto);
    }
}
