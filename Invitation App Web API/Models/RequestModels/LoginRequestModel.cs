using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invitation_App_Web_API.Models.RequestModels
{
    public class LoginRequestModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
