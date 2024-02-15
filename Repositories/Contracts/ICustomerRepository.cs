using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Task<PagedList<Customer>> GetAllCustomersAsync(CustomerParameters customerParameters,
            bool trackChanges);
        Task<List<Customer>> GetAllCustomersAsync(bool trackChanges);
        Task<Customer> GetOneCustomerIdAsync(int id, bool trackChanges);
        void CreateOneCustomer(Customer customer);
        void UpdateOneCustomer(Customer customer);
        void DeleteOneCustomer(Customer customer);
    }
}
