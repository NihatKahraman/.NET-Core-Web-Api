using Entities.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace Repositories.EFCore.Extensions
{
    public static class CustomerRepositoryExtensions
    {
        
        public static IQueryable<Customer> Search(this IQueryable<Customer> customers,
            string searchTerm)
        {
            if(string.IsNullOrWhiteSpace(searchTerm))
                return customers;

            var lowerCaseTerm = searchTerm.Trim().ToLower(); // kara
            return customers
                .Where(b => b.Name
                .ToLower()
                .Contains(searchTerm));
        }

        public static IQueryable<Customer> Sort(this IQueryable<Customer> customers,
            string orderByQueryString)
        {
            if(string.IsNullOrWhiteSpace(orderByQueryString))
                return customers.OrderBy(b => b.Id);
            
            var orderQuery = OrderQueryBuilder
                .CreateOrderQuery<Customer>(orderByQueryString);

            if (orderQuery is null)
                return customers.OrderBy(b => b.Id);

            return customers.OrderBy(orderQuery);

        }
    }
}
