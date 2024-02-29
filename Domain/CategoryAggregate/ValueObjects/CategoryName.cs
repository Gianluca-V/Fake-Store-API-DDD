using Domain.Common.Models;
using System;
using System.Collections.Generic;
using Domain.Common.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CategoryAggregate.ValueObjects
{
    [ComplexType]
    public class CategoryName : ValueObject
    {
        public string value { get; private set; }

        private CategoryName(string Value)
        {
            ValueValidation(Value);
            value = Value;
        }
        private CategoryName(){}

        private void ValueValidation(string Value)
        {
            if (string.IsNullOrWhiteSpace(Value)) { throw new ValidationException("Category name can not be null"); }
        }

        public static CategoryName CreateName(string Value)
        {
            return new(Value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return value;
        }
    }
}
