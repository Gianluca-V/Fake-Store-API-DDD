using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductAggregate.ValueObjects
{
    [ComplexType]
    public class ProductId : ValueObject
    {
        public Guid value { get; private set; }

        private ProductId(Guid Value) 
        {
            value = Value;
        }
        private ProductId() {}

        public static ProductId CreateUnique()
        {
            return new ProductId(Guid.NewGuid());
        }
        public static ProductId Create(Guid value)
        {
            return new ProductId(value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return value;
        }
    }
}
