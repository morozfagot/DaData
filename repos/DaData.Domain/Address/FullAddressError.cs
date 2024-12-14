using DaData.Domain.Abstractions;

namespace DaData.Domain.Address
{
    public static class FullAddressError
    {
        public static Error InvalidRequest = new(
            "FullAddress.InvalidRequest",
            "incorrect data was sent");
    }
}
