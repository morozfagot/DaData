using DaData.Application.Address.Queries.ParseAddresses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DaData.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController(ISender sender, ILogger<AddressController> logger) : ControllerBase
    {
        /// <summary>
        /// Получение адреса
        /// </summary>
        /// 
        /// <returns>Получение нового адреса</returns>
        /// <remarks>
        /// </remarks>
        /// <param name="addressForStandardization">Адрес для стандартизации</param>
        /// <response code="201">Возвращение стандартизированного адреса</response>
        /// <response code="400">Адрес не найден</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetAddress([FromQuery]FullAddressQuery input, CancellationToken cancellationToken)
        {
            var result = await sender.Send(input, cancellationToken);
             
            if (result.IsSuccess == true) 
            {
                logger.LogInformation("Successfully created address.");
                return Ok(result.Value);
            }
            else
            {
                logger.LogInformation(result.Error.Name);
                return BadRequest(result.Error);
            }
        }
    }
}