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
    public class ReservationLinks : IReservationLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<ReservationDto> _dataShaper;

        public ReservationLinks(LinkGenerator linkGenerator,
            IDataShaper<ReservationDto> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<ReservationDto> reservationDto,
            string fields,
            HttpContext httpContext)
        {
            var shapedReservations = ShapeData(reservationDto, fields);
            if (ShouldGenerateLinks(httpContext))
                return ReturnLinkedReservations(reservationDto, fields, httpContext, shapedReservations);
            return ReturnShapedReservations(shapedReservations);
        }

        private LinkResponse ReturnLinkedReservations(IEnumerable<ReservationDto> reservationDto,
            string fields,
            HttpContext httpContext,
            List<Entity> shapedReservations)
        {
            var reservationDtoList = reservationDto.ToList();

            for (int index = 0; index < reservationDtoList.Count(); index++)
            {
                var reservationLinks = CreateForReservation(httpContext, reservationDtoList[index], fields);
                shapedReservations[index].Add("Links", reservationLinks);
            }

            var reservationCollection = new LinkCollectionWrapper<Entity>(shapedReservations);
            CreateForReservation(httpContext, reservationCollection);
            return new LinkResponse { HasLinks = true, LinkedEntities = reservationCollection };
        }

        private LinkCollectionWrapper<Entity> CreateForReservation(HttpContext httpContext,
            LinkCollectionWrapper<Entity> reservationCollectionWrapper)
        {
            reservationCollectionWrapper.Links.Add(new Link()
            {
                Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}",
                Rel = "self",
                Method = "GET"
            });
            return reservationCollectionWrapper;
        }

        private List<Link> CreateForReservation(HttpContext httpContext,
            ReservationDto reservationDto,
            string fields)
        {
            var links = new List<Link>()
            {
               new Link()
               {
                   Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}" +
                   $"/{reservationDto.Id}",
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

        private LinkResponse ReturnShapedReservations(List<Entity> shapedReservations)
        {
            return new LinkResponse() { ShapedEntities = shapedReservations };
        }

        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];
            return mediaType
                .SubTypeWithoutSuffix
                .EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private List<Entity> ShapeData(IEnumerable<ReservationDto> reservationDto, string fields)
        {
            return _dataShaper
                .ShapeData(reservationDto, fields)
                .Select(b => b.Entity)
                .ToList();
        }


    }
}