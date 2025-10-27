﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiBaseController:ControllerBase
    {
        protected string GetEmailFromToken()
        {
            return User.FindFirstValue(ClaimTypes.Email)!;
        }
    }
}
