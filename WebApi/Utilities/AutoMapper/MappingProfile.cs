using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApi.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDtoForUpdate, Book>().ReverseMap();
            CreateMap<Book, BookDto>();
            CreateMap<BookDtoForInsertion, Book>();
            CreateMap<UserForRegistrationDto, User>();

            CreateMap<CustomerDtoForUpdate, Customer>().ReverseMap();
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDtoForInsertion, Customer>();

            CreateMap<ReservationDtoForUpdate, Reservation>().ReverseMap();
            CreateMap<Reservation, ReservationDto>();
            CreateMap<ReservationDtoForInsertion, Reservation>();
        }
    }
}
