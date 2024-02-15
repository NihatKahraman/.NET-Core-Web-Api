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
    public class CustomerManager : ICustomerService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly ICustomerLinks _customerLinks;

        public CustomerManager(IRepositoryManager manager,
            ILoggerService logger,
            IMapper mapper,
            ICustomerLinks customerLinks)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _customerLinks = customerLinks;
        }

        public async Task<CustomerDto> CreateOneCustomerAsync(CustomerDtoForInsertion customerDto)
        {
            var entity = _mapper.Map<Customer>(customerDto);
            _manager.Customer.CreateOneCustomer(entity);
            await _manager.SaveAsync();
            return _mapper.Map<CustomerDto>(entity);
            
        }

        public async Task DeleteOneCustomerAsync(int id, bool trackChanges)
        {
            var entity = await GetOneCustomerByIdAndCheckExists(id, trackChanges);
            _manager.Customer.DeleteOneCustomer(entity);
            await _manager.SaveAsync();
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)>
            GetAllCustomersAsync(LinkParameters linkParameters,
            bool trackChanges)
        {

            var customersWithMetaData = await _manager
                .Customer.GetAllCustomersAsync(linkParameters.CustomerParameters, trackChanges);

            var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customersWithMetaData);
            var links = _customerLinks.TryGenerateLinks(customersDto,
                linkParameters.CustomerParameters.Fields,
                linkParameters.HttpContext);

            return (linkResponse: links, metaData: customersWithMetaData.MetaData);
        }


        public async Task<List<Customer>> GetAllCustomersAsync(bool trackChanges)
        {
           var customers = await _manager.Customer.GetAllCustomersAsync(trackChanges);
           return customers;
        }

        public async Task<CustomerDto> GetOneCustomerByIdAsync(int id, bool trackChanges)
        {
            var customer = await GetOneCustomerByIdAndCheckExists(id, trackChanges);

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<(CustomerDtoForUpdate customerDtoForUpdate, Customer customer)>
            GetOneCustomerForPatchAsync(int id, bool trackChanges)
        {
            var customer = await GetOneCustomerByIdAndCheckExists(id, trackChanges);
            var customerDtoForUpdate = _mapper.Map<CustomerDtoForUpdate>(customer);
            return (customerDtoForUpdate, customer);
        }

        public async Task SaveChangesForPatchAsync(CustomerDtoForUpdate customerDtoForUpdate, Customer customer)
        {
            _mapper.Map(customerDtoForUpdate, customer);
            await _manager.SaveAsync();
        }

        public async Task UpdateOneCustomerAsync(int id,
            CustomerDtoForUpdate customerDto, 
            bool trackChanges)
        {
            // check entity
            var entity = await GetOneCustomerByIdAndCheckExists(id, trackChanges);

            //Mapping
            entity = _mapper.Map<Customer>(customerDto);

            _manager.Customer.Update(entity);
            await _manager.SaveAsync();
        }

        private async Task<Customer> GetOneCustomerByIdAndCheckExists(int id, bool trackChanges)
        {
            //check entity

            var entity = await _manager.Customer.GetOneCustomerIdAsync(id, trackChanges);

            if (entity is null)
                throw new CustomerNotFoundException(id);

            return entity;
        }

    }
}
