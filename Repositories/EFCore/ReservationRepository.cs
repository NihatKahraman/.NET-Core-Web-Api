using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public sealed class ReservationRepository : RepositoryBase<Reservation>, IReservationRepository
    {
        public ReservationRepository(RepositoryContext context) : base(context)
        {
            
        }
        
        public void CreateOneReservation(Reservation reservation) => Create(reservation);
        public void DeleteOneReservation(Reservation reservation) => Delete(reservation);
        public async Task<PagedList<Reservation>> GetAllReservationsAsync(ReservationParameters reservationParameters,
            bool trackChanges)
        {
             var reservations = await FindAll(trackChanges)
            .Search(reservationParameters.SearchTerm)
            .Sort(reservationParameters.OrderBy)
            .ToListAsync();

            return PagedList<Reservation>
                .ToPagedList(reservations,
                reservationParameters.PageNumber,
                reservationParameters.PageSize);
        }

        public async Task<List<Reservation>> GetAllReservationsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(b =>b.Id)
                .ToListAsync();
        }

        public async Task<Reservation> GetOneReservationIdAsync(int id, bool trackChanges) =>
            await FindByCondition(b => b.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
           
        

        public void UpdateOneReservation(Reservation reservation) => Update(reservation);
        
    }
}
