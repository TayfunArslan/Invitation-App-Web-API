using System;
using System.Threading.Tasks;
using Invitation_App_Web_API.Data.Entities;
using Invitation_App_Web_API.Data.Repositories;

namespace Invitation_App_Web_API.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<User> UserRepository { get; }
        public IRepository<Organization> OrganizationRepository { get; }
        public IRepository<Guest> GuestRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
