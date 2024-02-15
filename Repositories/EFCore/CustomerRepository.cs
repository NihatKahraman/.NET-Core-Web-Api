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
    public sealed class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(RepositoryContext context) : base(context)
        {
            
        }
        
        public void CreateOneCustomer(Customer customer) => Create(customer);
        public void DeleteOneCustomer(Customer customer) => Delete(customer);
        public async Task<PagedList<Customer>> GetAllCustomersAsync(CustomerParameters customerParameters,
            bool trackChanges)
        {
             var customers = await FindAll(trackChanges)
            .Search(customerParameters.SearchTerm)
            .Sort(customerParameters.OrderBy)
            .ToListAsync();

            return PagedList<Customer>
                .ToPagedList(customers,
                customerParameters.PageNumber,
                customerParameters.PageSize);
        }

        public async Task<List<Customer>> GetAllCustomersAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(b =>b.Id)
                .ToListAsync();
        }

        public async Task<Customer> GetOneCustomerIdAsync(int id, bool trackChanges) =>
            await FindByCondition(b => b.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
           
        

        public void UpdateOneCustomer(Customer customer) => Update(customer);
        
    }
}
