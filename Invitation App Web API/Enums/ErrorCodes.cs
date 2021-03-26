using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invitation_App_Web_API.Enums
{
    public enum ErrorCodes
    {
        UserExist = 1001,
        UserNotFound = 1002,
        IncorrectPassword = 1003,
        DbError = 3001,
        UnknownError = 4000
    }
}
