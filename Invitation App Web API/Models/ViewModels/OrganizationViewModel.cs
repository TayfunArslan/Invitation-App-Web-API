using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invitation_App_Web_API.Models.ViewModels
{
    public class OrganizationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public long CreatorId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
