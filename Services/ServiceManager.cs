using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;
        private readonly Lazy<ICustomerService> _customerService;
        private readonly Lazy<IReservationService> _reservationService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(IRepositoryManager repositoryManager,
            ILoggerService logger,
            IMapper mapper,
            IConfiguration configuration,
            UserManager<User> userManager,
            IBookLinks bookLinks,
            ICustomerLinks customerLinks,
            IReservationLinks reservationLinks)
        {
            _bookService = new Lazy<IBookService>(() => 
            new BookManager(repositoryManager, logger, mapper, bookLinks));

            _customerService = new Lazy<ICustomerService>(() =>
            new CustomerManager(repositoryManager, logger, mapper, customerLinks));

            _reservationService = new Lazy<IReservationService>(() =>
            new ReservationManager(repositoryManager, logger, mapper, reservationLinks));

            _authenticationService = new Lazy<IAuthenticationService>(() =>
            new AuthenticationManager(logger, mapper, userManager, configuration));
        }
        public IBookService BookService => _bookService.Value;
        public ICustomerService CustomerService => _customerService.Value;
        public IReservationService ReservationService => _reservationService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
