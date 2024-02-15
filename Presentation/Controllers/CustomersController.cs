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
    [Route("api/customers")]
    //[ApiExplorerSettings(GroupName = "Customers")]
    //[ResponseCache(CacheProfileName = "5mins")]
    public class CustomersController : ControllerBase
    {
        
        private readonly IServiceManager _manager;

        public CustomersController(IServiceManager manager)
        {
            _manager = manager;
        }

        [Authorize]
        [HttpHead]
        [HttpGet(Name = "GetAllCustomersAsync")]
        //[ResponseCache(Duration = 60)]
        //[HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 80)]
        public async Task<IActionResult> GetAllCustomersAsync([FromQuery] CustomerParameters customerParameters )
        {
            var linkParameters = new LinkParameters()
            {
                CustomerParameters = customerParameters,
                HttpContext = HttpContext

            };

            var result = await _manager
                .CustomerService
                .GetAllCustomersAsync(linkParameters, false);

            Response.Headers.Add("X-Pagination", 
                JsonSerializer.Serialize(result.metaData));

            return result.linkResponse.HasLinks ?
                Ok(result.linkResponse.LinkedEntities) :
                Ok(result.linkResponse.ShapedEntities);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneCustomerAsync([FromRoute(Name = "id")] int id)
        {
            var customers = await _manager
            .CustomerService
            .GetOneCustomerByIdAsync(id, false);
           
            return Ok(customers);
            
        }

        [Authorize(Roles = "Editor, Admin")]
        [HttpPost(Name = "CreateOneCustomerAsync")]
        public async Task<IActionResult> CreateOneCustomerAsync([FromBody] CustomerDtoForInsertion customerDto)
        {
            var customer = await _manager.CustomerService.CreateOneCustomerAsync(customerDto);
            return StatusCode(201, customer); // CreatedAtRoute()
        }

        [Authorize(Roles = "Editor, Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneCustomerAsync([FromRoute(Name = "id")] int id,
            [FromBody] CustomerDtoForUpdate customerDto)
        {

            await _manager.CustomerService.UpdateOneCustomerAsync(id, customerDto, false);
            return NoContent(); // 204

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneCustomerAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.CustomerService.DeleteOneCustomerAsync(id, false);
            return NoContent();
        }
        
        [Authorize(Roles = "Editor, Admin")]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateOneCustomerAsync([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<CustomerDtoForUpdate> customerPatch)
        {
            if(customerPatch is null)
                return BadRequest(); // 400

            var result = await _manager.CustomerService.GetOneCustomerForPatchAsync(id, false);

            customerPatch.ApplyTo(result.customerDtoForUpdate, ModelState);

            TryValidateModel(result.customerDtoForUpdate);

            if(!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

             await _manager.CustomerService.SaveChangesForPatchAsync(result.customerDtoForUpdate, result.customer);

            return NoContent(); //204

        }

        
        [Authorize]
        [HttpOptions]
        public IActionResult GetCustomerOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, PATCH, DELETE, HEAD, OPTIONS");
            return Ok();
        }


    }
}
