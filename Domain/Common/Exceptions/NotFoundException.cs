using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message) { }

    }
}
