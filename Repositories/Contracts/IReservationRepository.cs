using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IReservationRepository : IRepositoryBase<Reservation>
    {
        Task<PagedList<Reservation>> GetAllReservationsAsync(ReservationParameters reservationParameters,
            bool trackChanges);
        Task<List<Reservation>> GetAllReservationsAsync(bool trackChanges);
        Task<Reservation> GetOneReservationIdAsync(int id, bool trackChanges);
        void CreateOneReservation(Reservation reservation);
        void UpdateOneReservation(Reservation reservation);
        void DeleteOneReservation(Reservation reservation);
    }
}
