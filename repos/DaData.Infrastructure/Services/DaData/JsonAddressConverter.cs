using DaData.Domain.Abstractions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using DaData.Domain.Address;

namespace DaData.Infrastructure.Services.DaData
{
    internal class JsonAddressConverter : JsonConverter<Result<FullAddress>>
    {
        public override Result<FullAddress>? ReadJson(JsonReader reader, Type objectType, Result<FullAddress>? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);

            string country = jsonObject["Country"].Value<string>();
            string region = jsonObject["Region"].Value<string>();
            string street = jsonObject["Street"].Value<string>();
            int houseNamber = jsonObject["HouseNamber"].Value<int>();
            int roomNamber = jsonObject["RoomNamber"].Value<int>();
            int postalCode = jsonObject["PostalCode"].Value<int>();

            var fullAddress = FullAddress.Create(country, region, street, houseNamber, roomNamber, postalCode);

            return fullAddress;
        }

        public override void WriteJson(JsonWriter writer, Result<FullAddress>? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
