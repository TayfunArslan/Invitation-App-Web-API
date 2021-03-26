using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invitation_App_Web_API.Enums;
using Invitation_App_Web_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace Invitation_App_Web_API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult ReturnActionResult<T>(ServiceResult<T> serviceResult)
        {
            if (serviceResult.ServiceResultType == ServiceResultType.Fail)
                return BadRequest(serviceResult.ErrorModel);

            return Ok(serviceResult.Data);
        }
        protected int GetUserId()
        {
            return 0;
        }
    }
}
