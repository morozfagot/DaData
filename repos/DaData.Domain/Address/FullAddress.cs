using DaData.Domain.Abstractions;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using DaData.Domain.Address.Events;
using DaData.Domain.Address.ValueObjects;

namespace DaData.Domain.Address
{
    public sealed class FullAddress : Entity
    {
        private FullAddress(
            Guid id,
            Country country,
            Sity sity,
            Street street,
            int houseNamber,
            int roomNamber,
            int postalCode)
            : base(id)
        {
            Country = country;
            Sity = sity;
            Street = street;
            HouseNumber = houseNamber;
            RoomNumber = roomNamber;
            PostalCode = postalCode;
        }

        public Country Country { get; private set; }
        public Sity Sity { get; private set; }
        public Street Street { get; private set; }
        public int HouseNumber { get; private set; }
        public int RoomNumber { get; private set; }
        public int PostalCode { get; private set; }

        public static async Task<Result<FullAddress>> AddressStandartization(AddressForStandartization addressForStandartization, IServiceProvider serviceProvider)
        {
            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.GetHttpClient();

            var daDataKeys = serviceProvider.GetRequiredService<IOptions<DaDatakeys>>().Value;
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
