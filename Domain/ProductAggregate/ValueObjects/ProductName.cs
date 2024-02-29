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
    public class ProductName : ValueObject
    {
        public string value { get; private set; }
        private ProductName(string Value)
        {
            ValueValidation(Value);
            value = Value;
        }
        private ProductName() {}

        private void ValueValidation(string Value)
        {
            if (string.IsNullOrWhiteSpace(Value)) { throw new ValidationException("Product name can not be null"); }
        }

        public static ProductName CreateName(string Value)
        {
            return new ProductName(Value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return value;
        }
    }
}
