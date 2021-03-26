using System;
using System.Collections.Generic;

namespace Invitation_App_Web_API.Data.Entities
{
    public class Organization : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public long CreatorId { get; set; }

        public User Creator { get; set; }
        public ICollection<Guest> Guests { get; set; }
    }
}
