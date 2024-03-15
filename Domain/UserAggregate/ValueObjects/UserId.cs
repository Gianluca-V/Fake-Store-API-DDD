using Domain.Common.Models;
using Domain.ProductAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserAggregate.ValueObjects
{
    [ComplexType]
    public class UserId : ValueObject
    {
        public Guid value { get; private set;}

        private UserId() { }
        private UserId(Guid Value)
        {
            value = Value;
        }

        public static UserId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static UserId Create(Guid value)
        {
            return new UserId(value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return value;
        }
    }
}
