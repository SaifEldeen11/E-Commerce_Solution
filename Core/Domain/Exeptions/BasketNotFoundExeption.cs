using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public sealed class BasketNotFoundExeption(string key ):NotFoundExeption($"Basket with id = {key} is not found ")
    {

    }
}
