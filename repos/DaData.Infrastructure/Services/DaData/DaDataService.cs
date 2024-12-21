using DaData.Domain.Abstractions;
using DaData.Domain.Address;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DaData.Infrastructure.Services.DaData
{
    public class DaDataService(HttpClient httpClient, IOptions<DaDatakeys> options) : IDaDataService
    {
        public async Task<Result<FullAddress>> GetAddress(string addressForStandardization)
        {
            var daDataKeys = options.Value;
            var token = daDataKeys.Token;
            var secret = daDataKeys.Secret;

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);
            httpClient.DefaultRequestHeaders.Add("X-Secret", secret);

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestBody = new
            {
                data = addressForStandardization
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://cleaner.dadata.ru/api/v1/clean/address", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var jsonSettings = new JsonSerializerSettings();
                jsonSettings.Converters.Add(new JsonAddressConverter());

                var result = JsonConvert.DeserializeObject<Result<FullAddress>>(jsonResponse);

                return result;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");

                return Result.Failure<FullAddress>(FullAddressError.InvalidRequest);
            }
        }
    }
}
