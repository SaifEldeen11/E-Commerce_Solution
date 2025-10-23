using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public sealed class AddressNotFoundException(string userName):Exception($"user {userName} has no Adreess")
    {

    }
}
