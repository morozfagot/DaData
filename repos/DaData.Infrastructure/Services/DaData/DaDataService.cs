using DaData.Domain.Abstractions;
using DaData.Domain.Address;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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

            var json = JsonConvert.SerializeObject(new string[] { addressForStandardization });

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://cleaner.dadata.ru/api/v1/clean/address");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Token", token);
            requestMessage.Headers.Add("X-Secret", secret);
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var jsonSettings = new JsonSerializerSettings();
                jsonSettings.Converters.Add(new JsonAddressConverter());

                var result = JsonConvert.DeserializeObject<Result<FullAddress>>(jsonResponse, jsonSettings);

                return result;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                return Result.Failure<FullAddress>(FullAddressError.InvalidRequest);
            }
        }
    }
}
