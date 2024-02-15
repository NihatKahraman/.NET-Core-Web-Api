using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IReservationService
    {
        Task<(LinkResponse linkResponse, MetaData metaData)> GetAllReservationsAsync(LinkParameters linkParameters, bool trackChanges);
        Task<ReservationDto> GetOneReservationByIdAsync(int id, bool trackChanges);
        Task<ReservationDto> CreateOneReservationAsync(ReservationDtoForInsertion reservation);
        Task UpdateOneReservationAsync(int id, ReservationDtoForUpdate reservationDto, bool trackChanges);
        Task DeleteOneReservationAsync(int id, bool trackChanges);

        Task<(ReservationDtoForUpdate reservationDtoForUpdate, Reservation reservation)> GetOneReservationForPatchAsync(int id, bool trackChanges);

        Task SaveChangesForPatchAsync(ReservationDtoForUpdate reservationDtoForUpdate, Reservation reservation);
        Task<List<Reservation>> GetAllReservationsAsync(bool trackChanges);
    }
}
