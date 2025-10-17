using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public class ProductNotFoundExeption(int id) : NotFoundExeption($"Product with Id :{id} is not found ")
    {
        
    }
}
