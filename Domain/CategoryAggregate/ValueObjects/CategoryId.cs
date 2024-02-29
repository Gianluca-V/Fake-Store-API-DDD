using Domain.Common.Models;
using Domain.ProductAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CategoryAggregate.ValueObjects
{
    [ComplexType]
    public class CategoryId : ValueObject
    {
        public Guid value { get; private set; }

        private CategoryId(Guid Value)
        {
            value = Value;
        }
        private CategoryId() { }

        public static CategoryId CreateUnique()
        {
            return new CategoryId(Guid.NewGuid());
        }

        public static CategoryId Create(Guid value)
        {
            return new CategoryId(value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return value;
        }
    }
}
