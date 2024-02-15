using Entities.DataTransferObjects;
using Entities.LinkModels;
using Microsoft.AspNetCore.Http;

namespace Services.Contracts
{
    public interface IReservationLinks
    {
        LinkResponse TryGenerateLinks(IEnumerable<ReservationDto> reservationDto,
            string fields, HttpContext httpContext);
    }
}
