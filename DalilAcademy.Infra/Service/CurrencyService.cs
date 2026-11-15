using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace DalilAcademy.Infra.Service
{
    public class CurrencyService(HttpClient http) : ICurrencyService
    {
        private record PairResponse([property: JsonPropertyName("conversion_rate")] decimal ConversionRate);
        public async Task<ConvertPriceResponseDto> ConvertAsync(ConvertPriceRequestDto r) 
        {
            // BaseAddress already holds the key — we only add the endpoint path
            var data = await http.GetFromJsonAsync<PairResponse>($"pair/{r.FromCurrency}/{r.ToCurrency}")
            ?? throw new HttpRequestException("Empty response from ExchangeRate-API.");
            var converted = Math.Round(r.Amount * data.ConversionRate, 2);
            return new ConvertPriceResponseDto(r.Amount, r.FromCurrency, converted, r.ToCurrency, data.ConversionRate);
        }

    }
}
