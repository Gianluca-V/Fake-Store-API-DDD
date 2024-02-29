using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserAggregate.ValueObjects
{
    public class UserId : ValueObject
    {
        public Guid value { get; }

        private UserId(Guid Value)
        {
            value = Value;
        }

        public static UserId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return value;
        }
    }
}
