using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

namespace Sankhya.Mudra.Rupantraan
{
    public class CurrencyService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string ApiBase = "https://api.frankfurter.dev/v1";
        public CurrencyService()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }
        /// <summary>
        /// Fetches all available currency codes and their full names.
        /// </summary>
        public async Task<Dictionary<string, string>> GetCurrenciesAsync()
        {
            try
            {
                string url = $"{ApiBase}/currencies";
                string json = await _httpClient.GetStringAsync(url);
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            catch
            {
                // Return a basic list in case the API fails
                return new Dictionary<string, string>
                {
                    { "USD", "United States Dollar" },
                    { "EUR", "Euro" },
                    { "INR", "Indian Rupee" },
                    { "GBP", "British Pound" },
                    { "JPY", "Japanese Yen" },
                    { "AUD", "Australian Dollar" },
                    { "CAD", "Canadian Dollar" },
                    { "CHF", "Swiss Franc" },
                    { "CNY", "Chinese Yuan" },
                    { "NZD", "New Zealand Dollar" },
                    { "ZAR", "South African Rand" },
                    { "BRL", "Brazilian Real" },
                    { "RUB", "Russian Ruble" },
                    { "SGD", "Singapore Dollar" },
                    { "HKD", "Hong Kong Dollar" }
                };
            }
        }

        /// <summary>
        /// Converts an amount from one currency to another.
        /// </summary>
        /// <param name="fromCurrency">The currency code to convert from.</param>
        /// <param name="toCurrency">The currency code to convert to.</param>
        /// <param name="amount">The amount to convert.</param>
        /// <returns>The converted amount.</returns>
        /// <remarks>
        /// This method uses the Frankfurter API to perform the currency conversion.
        /// </remarks>
        public async Task<decimal> ConvertCurrencyAsync(string fromCurrency, string toCurrency, decimal amount)
        {
            if (fromCurrency == toCurrency) return amount;

            try
            {
                string url = $"{ApiBase}/latest?amount={amount}&from={fromCurrency}&to={toCurrency}";
                string json = await _httpClient.GetStringAsync(url);

                // Deserialize into our helper model
                var response = JsonConvert.DeserializeObject<ConversionResponse>(json);

                if (response?.Rates != null && response.Rates.ContainsKey(toCurrency))
                {
                    return response.Rates[toCurrency];
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        private class ConversionResponse
        {
            [JsonProperty("amount")]
            public decimal Amount { get; set; }

            [JsonProperty("base")]
            public string Base { get; set; }

            [JsonProperty("date")]
            public string Date { get; set; }

            [JsonProperty("rates")]
            public Dictionary<string, decimal> Rates { get; set; }
        }
    }
}
