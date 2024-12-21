using DaData.Application.Address.Queries.FullAddress;
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
        /// Creates a TodoItem.
        /// </summary>
        /// 
        /// <returns>A newly created TodoItem</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/address/create
        ///     {
        ///        "id": 1,
        ///        "AddressForStandardization": "мск сухонска 11/-89"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        /// <response code="500">InternalServerError</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(FullAddressQuery input, CancellationToken cancellationToken)
        {
            Console.WriteLine("jopiiiiiiiiiiiiiiiiiiiiii");
            var result = await sender.Send(input, cancellationToken);
             
            if (result.IsSuccess == true) 
            {
                logger.LogInformation("Successfully created address.");
                return Created(string.Empty, result.Value);
            }
            else
            {
                logger.LogInformation(result.Error.Name);
                return BadRequest(result.Error);
            }
        }
    }
}