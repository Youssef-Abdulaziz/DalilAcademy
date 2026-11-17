using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using DalilAcademy.Core.Payments;
using Microsoft.Extensions.Options;

namespace DalilAcademy.Infra.Payments
{
    public class PayPalPaymentGateway(HttpClient http, IOptions<PayPalSettings> options) : IPaymentGateway
    {
        private readonly PayPalSettings settings = options.Value;

        // Call 1: access token (client credentials)
        private async Task<string> GetAccessTokenAsync()
        {
            var credentials = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{settings.ClientId}:{settings.ClientSecret}"));

            var request = new HttpRequestMessage(HttpMethod.Post, "/v1/oauth2/token")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["grant_type"] = "client_credentials"
                })
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            var response = await http.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            return json.GetProperty("access_token").GetString()!;
        }

        // Call 2: create order
        public async Task<GatewayOrder> CreateOrderAsync(decimal amountUsd)
        {
            var token = await GetAccessTokenAsync();

            var body = new
            {
                intent = "CAPTURE",
                purchase_units = new[]
                {
                    new
                    {
                        amount = new
                        {
                            currency_code = "USD",
                            value = amountUsd.ToString("F2", CultureInfo.InvariantCulture)
                        }
                    }
                },
                payment_source = new
                {
                    paypal = new
                    {
                        experience_context = new
                        {
                            return_url = settings.ReturnUrl,
                            cancel_url = settings.ReturnUrl
                        }
                    }
                }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "/v2/checkout/orders")
            {
                Content = JsonContent.Create(body)
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await http.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            var orderId = json.GetProperty("id").GetString()!;

            var approvalUrl = json.GetProperty("links").EnumerateArray()
                .First(l => l.GetProperty("rel").GetString() is "payer-action" or "approve")
                .GetProperty("href").GetString()!;

            return new GatewayOrder(orderId, approvalUrl);
        }

        // Call 3: capture
        public async Task<bool> CaptureOrderAsync(string orderId)
        {
            var token = await GetAccessTokenAsync();

            var request = new HttpRequestMessage(HttpMethod.Post, $"/v2/checkout/orders/{orderId}/capture")
            {
                Content = new StringContent("{}", Encoding.UTF8, "application/json")
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await http.SendAsync(request);

            // Not approved yet, already captured, expired: a failure, not an exception
            if (!response.IsSuccessStatusCode)
                return false;

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            return json.TryGetProperty("status", out var status)
                && status.GetString() == "COMPLETED";
        }
    }
}