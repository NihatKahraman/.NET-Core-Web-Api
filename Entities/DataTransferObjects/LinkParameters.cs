using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;

namespace Entities.DataTransferObjects
{
    public record LinkParameters
    {
        public BookParameters BookParameters { get; init; }
        public CustomerParameters CustomerParameters { get; init; }
        public ReservationParameters ReservationParameters { get; init; }
        public HttpContext HttpContext { get; init; }
    }
}
