﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public abstract class NotFoundExeption(string message ) : Exception(message)
    {

    }
}
