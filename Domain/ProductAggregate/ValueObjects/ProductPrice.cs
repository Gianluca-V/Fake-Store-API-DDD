using Domain.Common.Models;
using System;
using System.Collections.Generic;
using Domain.Common.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductAggregate.ValueObjects
{
    [ComplexType]
    public class ProductPrice : ValueObject
    {
        public float value { get; private set; }
        private ProductPrice(float Value) 
        {
            ValueValidation(Value);
            value = Value;
        }
        private ProductPrice() {}

        private void ValueValidation(float Value)
        {
            if (Value < 0) { throw new ValidationException("Product price can not be negative"); }
        }

        public static ProductPrice CreatePrice(float Value)
        {
            return new ProductPrice(Value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return value;
        }
    }
}
