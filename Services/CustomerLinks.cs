using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Services.Contracts;
using System.ComponentModel.Design;

namespace Services
{
    public class CustomerLinks : ICustomerLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<CustomerDto> _dataShaper;

        public CustomerLinks(LinkGenerator linkGenerator,
            IDataShaper<CustomerDto> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<CustomerDto> customerDto,
            string fields,
            HttpContext httpContext)
        {
            var shapedCustomers = ShapeData(customerDto, fields);
            if (ShouldGenerateLinks(httpContext))
                return ReturnLinkedCustomers(customerDto, fields, httpContext, shapedCustomers);
            return ReturnShapedCustomers(shapedCustomers);
        }

        private LinkResponse ReturnLinkedCustomers(IEnumerable<CustomerDto> customerDto,
            string fields,
            HttpContext httpContext,
            List<Entity> shapedCustomers)
        {
            var customerDtoList = customerDto.ToList();

            for (int index = 0; index < customerDtoList.Count(); index++)
            {
                var customerLinks = CreateForCustomer(httpContext, customerDtoList[index], fields);
                shapedCustomers[index].Add("Links", customerLinks);
            }

            var customerCollection = new LinkCollectionWrapper<Entity>(shapedCustomers);
            CreateForCustomer(httpContext, customerCollection);
            return new LinkResponse { HasLinks = true, LinkedEntities = customerCollection };
        }

        private LinkCollectionWrapper<Entity> CreateForCustomer(HttpContext httpContext,
            LinkCollectionWrapper<Entity> customerCollectionWrapper)
        {
            customerCollectionWrapper.Links.Add(new Link()
            {
                Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}",
                Rel = "self",
                Method = "GET"
            });
            return customerCollectionWrapper;
        }

        private List<Link> CreateForCustomer(HttpContext httpContext,
            CustomerDto customerDto,
            string fields)
        {
            var links = new List<Link>()
            {
               new Link()
               {
                   Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}" +
                   $"/{customerDto.Id}",
                   Rel = "self",
                   Method = "GET"
               },
               new Link()
               {
                   Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}",
                   Rel="create",
                   Method = "POST"
               },
            };
            return links;
        }

        private LinkResponse ReturnShapedCustomers(List<Entity> shapedCustomers)
        {
            return new LinkResponse() { ShapedEntities = shapedCustomers };
        }

        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];
            return mediaType
                .SubTypeWithoutSuffix
                .EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private List<Entity> ShapeData(IEnumerable<CustomerDto> customerDto, string fields)
        {
            return _dataShaper
                .ShapeData(customerDto, fields)
                .Select(b => b.Entity)
                .ToList();
        }


    }
}