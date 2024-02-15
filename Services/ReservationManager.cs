using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.Exceptions.BadRequestException;
using static System.Reflection.Metadata.BlobBuilder;

namespace Services
{
    public class ReservationManager : IReservationService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IReservationLinks _reservationLinks;

        public ReservationManager(IRepositoryManager manager,
            ILoggerService logger,
            IMapper mapper,
            IReservationLinks reservationLinks)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _reservationLinks = reservationLinks;
        }

        public async Task<ReservationDto> CreateOneReservationAsync(ReservationDtoForInsertion reservationsDto)
        {
            var entity = _mapper.Map<Reservation>(reservationsDto);
            _manager.Reservation.CreateOneReservation(entity);
            await _manager.SaveAsync();
            return _mapper.Map<ReservationDto>(entity);
            
        }

        public async Task DeleteOneReservationAsync(int id, bool trackChanges)
        {
            var entity = await GetOneReservationByIdAndCheckExists(id, trackChanges);
            _manager.Reservation.DeleteOneReservation(entity);
            await _manager.SaveAsync();
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)>
            GetAllReservationsAsync(LinkParameters linkParameters,
            bool trackChanges)
        {
            
            var reservationsWithMetaData = await _manager
                .Reservation.GetAllReservationsAsync(linkParameters.ReservationParameters, trackChanges);

            var reservationsDto =  _mapper.Map<IEnumerable<ReservationDto>>(reservationsWithMetaData);
            var links = _reservationLinks.TryGenerateLinks(reservationsDto,
                linkParameters.ReservationParameters.Fields,
                linkParameters.HttpContext);

            return (linkResponse : links, metaData: reservationsWithMetaData.MetaData);
        }

        public async Task<List<Reservation>> GetAllReservationsAsync(bool trackChanges)
        {
           var reservations = await _manager.Reservation.GetAllReservationsAsync(trackChanges);
           return reservations;
        }

        public async Task<ReservationDto> GetOneReservationByIdAsync(int id, bool trackChanges)
        {
            var reservation = await GetOneReservationByIdAndCheckExists(id, trackChanges);

            return _mapper.Map<ReservationDto>(reservation);
        }

        public async Task<(ReservationDtoForUpdate reservationDtoForUpdate, Reservation reservation)>
            GetOneReservationForPatchAsync(int id, bool trackChanges)
        {
            var reservation = await GetOneReservationByIdAndCheckExists(id, trackChanges);
            var reservationDtoForUpdate = _mapper.Map<ReservationDtoForUpdate>(reservation);
            return (reservationDtoForUpdate, reservation);
        }

        public async Task SaveChangesForPatchAsync(ReservationDtoForUpdate reservationDtoForUpdate, Reservation reservation)
        {
            _mapper.Map(reservationDtoForUpdate, reservation);
            await _manager.SaveAsync();
        }

        public async Task UpdateOneReservationAsync(int id,
            ReservationDtoForUpdate reservationDto, 
            bool trackChanges)
        {
            // check entity
            var entity = await GetOneReservationByIdAndCheckExists(id, trackChanges);

            //Mapping
            entity = _mapper.Map<Reservation>(reservationDto);

            _manager.Reservation.Update(entity);
            await _manager.SaveAsync();
        }

        private async Task<Reservation> GetOneReservationByIdAndCheckExists(int id, bool trackChanges)
        {
            //check entity

            var entity = await _manager.Reservation.GetOneReservationIdAsync(id, trackChanges);

            if (entity is null)
                throw new ReservationNotFoundException(id);

            return entity;
        }

    }
}
