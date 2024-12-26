using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using DaData.Domain.Address;
using DaData.Domain.Abstractions;

namespace DaData.Infrastructure.Services.DaData
{
    internal class JsonAddressConverter : JsonConverter<Result<FullAddress>>
    {
        public override Result<FullAddress>? ReadJson(JsonReader reader, Type objectType, Result<FullAddress>? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JArray jsonArray = JArray.Load(reader);

            if (jsonArray.Count == 0 )
            {
                return Result.Failure<FullAddress>(FullAddressError.InvalidRequest);
            }

            JObject jsonObject = (JObject)jsonArray[0];

            string country = jsonObject["country"].Value<string>();
            string region = jsonObject["region"].Value<string>();
            string street = jsonObject["street"].Value<string>();
            int houseNamber = jsonObject["house"].Value<int>();
            int roomNamber = jsonObject["flat"].Value<int>();
            int postalCode = jsonObject["postal_code"].Value<int>();

            var fullAddress = FullAddress.Create(country, region, street, houseNamber, roomNamber, postalCode);

            return fullAddress;
        }

        public override void WriteJson(JsonWriter writer, Result<FullAddress>? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
