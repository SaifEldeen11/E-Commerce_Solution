﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public sealed class UserNotFoundExeption(string email ):NotFoundExeption($"Can't find user with this email {email} , try register")
    {

    }
}
