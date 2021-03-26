using System;
using System.Threading.Tasks;
using System.Transactions;
using Invitation_App_Web_API.Data.Entities;
using Invitation_App_Web_API.Data.Repositories;

namespace Invitation_App_Web_API.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed;
        private IRepository<User> _userRepository;
        private IRepository<Organization> _organizationRepository;
        private IRepository<Guest> _guestRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context ??= context;
        }

        public IRepository<User> UserRepository
        {
            get { return _userRepository ??= new Repository<User>(_context); }
        }

        public IRepository<Organization> OrganizationRepository
        {
            get { return _organizationRepository ??= new Repository<Organization>(_context); }
        }

        public IRepository<Guest> GuestRepository
        {
            get { return _guestRepository ??= new Repository<Guest>(_context); }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                using var tScope = new TransactionScope();

                await _context.SaveChangesAsync();
                tScope.Complete();

                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
