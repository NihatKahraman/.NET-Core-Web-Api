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
    public interface ICustomerService
    {
        Task<(LinkResponse linkResponse, MetaData metaData)> GetAllCustomersAsync(LinkParameters linkParameters, bool trackChanges);
        Task<CustomerDto> GetOneCustomerByIdAsync(int id, bool trackChanges);
        Task<CustomerDto> CreateOneCustomerAsync(CustomerDtoForInsertion customer);
        Task UpdateOneCustomerAsync(int id, CustomerDtoForUpdate customerDto, bool trackChanges);
        Task DeleteOneCustomerAsync(int id, bool trackChanges);

        Task<(CustomerDtoForUpdate customerDtoForUpdate, Customer customer)> GetOneCustomerForPatchAsync(int id, bool trackChanges);

        Task SaveChangesForPatchAsync(CustomerDtoForUpdate customerDtoForUpdate, Customer customer);
        Task<List<Customer>> GetAllCustomersAsync(bool trackChanges);
    }
}
