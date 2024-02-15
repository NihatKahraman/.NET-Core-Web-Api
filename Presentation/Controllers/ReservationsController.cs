using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Text.Json;

namespace Presentation.Controllers
{
    //[ApiVersion("1.0")]
    [Authorize]
    [ApiController]
    [Route("api/reservations")]
    //[ApiExplorerSettings(GroupName = "v1")]
    //[ResponseCache(CacheProfileName = "5mins")]
    public class ReservationsController : ControllerBase
    {
        
        private readonly IServiceManager _manager;

        public ReservationsController(IServiceManager manager)
        {
            _manager = manager;
        }

        [Authorize]
        [HttpHead]
        [HttpGet(Name = "GetAllReservationsAsync")]
        //[ResponseCache(Duration = 60)]
        //[HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 80)]
        public async Task<IActionResult> GetAllReservationsAsync([FromQuery] ReservationParameters reservationParameters )
        {
            var linkParameters = new LinkParameters()
            {
                ReservationParameters = reservationParameters,
                HttpContext = HttpContext

            };

            var result = await _manager
                .ReservationService
                .GetAllReservationsAsync(linkParameters, false);

            Response.Headers.Add("X-Pagination", 
                JsonSerializer.Serialize(result.metaData));

            return result.linkResponse.HasLinks ?
                Ok(result.linkResponse.LinkedEntities) :
                Ok(result.linkResponse.ShapedEntities);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneReservationAsync([FromRoute(Name = "id")] int id)
        {
            var reservations = await _manager
            .ReservationService
            .GetOneReservationByIdAsync(id, false);
           
            return Ok(reservations);
            
        }

        [Authorize(Roles = "Editor, Admin")]
        [HttpPost(Name = "CreateOneReservationAsync")]
        public async Task<IActionResult> CreateOneReservationAsync([FromBody] ReservationDtoForInsertion reservationDto)
        {
            var reservation = await _manager.ReservationService.CreateOneReservationAsync(reservationDto);
            return StatusCode(201, reservation); // CreatedAtRoute()
        }

        [Authorize(Roles = "Editor, Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneReservationAsync([FromRoute(Name = "id")] int id,
            [FromBody] ReservationDtoForUpdate reservationDto)
        {

            await _manager.ReservationService.UpdateOneReservationAsync(id, reservationDto, false);
            return NoContent(); // 204

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneReservationAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.ReservationService.DeleteOneReservationAsync(id, false);
            return NoContent();
        }
        
        [Authorize(Roles = "Editor, Admin")]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateOneReservationAsync([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<ReservationDtoForUpdate> reservationPatch)
        {
            if(reservationPatch is null)
                return BadRequest(); // 400

            var result = await _manager.ReservationService.GetOneReservationForPatchAsync(id, false);

            reservationPatch.ApplyTo(result.reservationDtoForUpdate, ModelState);

            TryValidateModel(result.reservationDtoForUpdate);

            if(!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

             await _manager.ReservationService.SaveChangesForPatchAsync(result.reservationDtoForUpdate, result.reservation);

            return NoContent(); //204

        }

        
        [Authorize]
        [HttpOptions]
        public IActionResult GetReservationOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, PATCH, DELETE, HEAD, OPTIONS");
            return Ok();
        }


    }
}
