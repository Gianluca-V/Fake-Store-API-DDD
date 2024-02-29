using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Exceptions
{
    public class AlreadyExistException : BaseException
    {
        public AlreadyExistException(string message) : base(message) { }
    }
}
