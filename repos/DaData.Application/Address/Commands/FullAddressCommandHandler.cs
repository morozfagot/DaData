using AutoMapper;
using DaData.Application.Abstractions.Messaging;
using DaData.Domain.Abstractions;
using DaData.Domain.Address;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace DaData.Application.Address.Commands
{
    internal sealed class FullAddressCommandHandler(IRepositoryManager repositoryManager,
        IMapper mapper, IServiceProvider serviceProvider)
        : ICommandHandler<FullAddressCommand, FullAddressDto>
    {
        public async Task<Result<FullAddressDto>> Handle(FullAddressCommand command,
            CancellationToken cancellationToken = default)
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
                data = command.addressForStandardization
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://cleaner.dadata.ru/api/v1/clean/address", content);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var dadataResult = JsonConvert.DeserializeObject<Result<FullAddress>>(jsonResponse);

            if(dadataResult.IsSuccess == false)
            {
                return Result.Failure<FullAddressDto>(FullAddressError.InvalidRequest);
            }

            var result = mapper.Map<FullAddressDto>(dadataResult.Value);

            return result;
        }
    }
}
