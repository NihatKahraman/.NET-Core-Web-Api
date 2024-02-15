using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {

        private readonly RepositoryContext _context;
        private readonly Lazy<IBookRepository> _bookRepository;
        private readonly Lazy<ICustomerRepository> _customerRepository;
        private readonly Lazy<IReservationRepository> _reservationRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _bookRepository = new Lazy<IBookRepository>(() => new BookRepository(_context));
            _customerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(_context));
            _reservationRepository = new Lazy<IReservationRepository>(() => new ReservationRepository(_context));
        }

        public IBookRepository Book => _bookRepository.Value;
        public ICustomerRepository Customer => _customerRepository.Value;
        public IReservationRepository Reservation => _reservationRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
