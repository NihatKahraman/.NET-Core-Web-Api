using Entities.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace Repositories.EFCore.Extensions
{
    public static class ReservationRepositoryExtensions
    {
        
        public static IQueryable<Reservation> Search(this IQueryable<Reservation> reservations,
            string searchTerm)
        {
            if(string.IsNullOrWhiteSpace(searchTerm))
                return reservations;

            var lowerCaseTerm = searchTerm.Trim().ToLower(); // kara
            return reservations
                .Where(b => b.Book
                .ToLower()
                .Contains(searchTerm));
        }

        public static IQueryable<Reservation> Sort(this IQueryable<Reservation> reservations,
            string orderByQueryString)
        {
            if(string.IsNullOrWhiteSpace(orderByQueryString))
                return reservations.OrderBy(b => b.Id);
            
            var orderQuery = OrderQueryBuilder
                .CreateOrderQuery<Reservation>(orderByQueryString);

            if (orderQuery is null)
                return reservations.OrderBy(b => b.Id);

            return reservations.OrderBy(orderQuery);

        }
    }
}
