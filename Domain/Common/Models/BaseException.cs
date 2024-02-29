using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Models
{
    public abstract class BaseException : Exception
    {
        public BaseException(string message) : base(message) { }
    }
}
