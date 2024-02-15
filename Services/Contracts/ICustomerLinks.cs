using Entities.DataTransferObjects;
using Entities.LinkModels;
using Microsoft.AspNetCore.Http;

namespace Services.Contracts
{
    public interface ICustomerLinks
    {
        LinkResponse TryGenerateLinks(IEnumerable<CustomerDto> customerDto,
            string fields, HttpContext httpContext);
    }
}
