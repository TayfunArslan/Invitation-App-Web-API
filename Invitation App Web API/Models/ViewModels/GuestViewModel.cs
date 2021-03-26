using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invitation_App_Web_API.Models.ViewModels
{
    public class GuestViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public long OrganizationId { get; set; }
        public bool IsCalled { get; set; }
        public bool IsEmailSent { get; set; }
        public bool WillCome { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
