using DaData.Domain.Abstractions;
using DaData.Domain.Address.Events;
using DaData.Domain.Address.Interfaces;
using DaData.Domain.Address.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace DaData.Domain.Address
{
    public class FullAddressFactory : IFullAddressFactory
    {
        private readonly IServiceProvider _serviceProvider;
        
        public FullAddressFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Result<FullAddress>> AddressStandardization(AddressForStandartization addressForStandartization,
            CancellationToken cancellationToken)
        {
            var httpClientFactory = _serviceProvider.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.GetHttpClient();

            var daDataKeys = _serviceProvider.GetRequiredService<IOptions<DaDatakeys>>().Value;
            var token = daDataKeys.Token;
            var secret = daDataKeys.Secret;

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);
            httpClient.DefaultRequestHeaders.Add("X-Secret", secret);

            var requestBody = new
            {
                data = addressForStandartization.addressForStandartization
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://cleaner.dadata.ru/api/v1/clean/address", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var dadataResponse = JsonConvert.DeserializeObject<DaDataResponce>(jsonResponse);

                var address = new FullAddress(
                    Guid.NewGuid(),
                    new Country(dadataResponse.Country),
                    new Sity(dadataResponse.Region),
                    new Street(dadataResponse.Street),
                    int.Parse(dadataResponse.House),
                    string.IsNullOrEmpty(dadataResponse.Room) ? 0 : int.Parse(dadataResponse.Room),
                    int.Parse(dadataResponse.PostalCode)
                );

                address.RaiseDomainEvent(new AddressStandartizedDomainEvent(address.Id));

                Result<FullAddress> result = address;

                return result;
            }

            return Result.Failure<FullAddress>(FullAddressError.InvalidRequest);
        }
    }
}
