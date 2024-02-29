using Domain.Common.Models;
using System;
using System.Collections.Generic;
using Domain.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.UserAggregate.ValueObjects
{
    public class UserEmail : ValueObject
    {
        public string value { get; }

        private UserEmail(string Value)
        {
            ValueValidation(Value);
            value = Value;
        }
        public static UserEmail CreateEmail(string Value)
        {
            return new UserEmail(Value);
        }

        private void ValueValidation(string Value)
        {
            if (string.IsNullOrEmpty(Value)) { throw new ValidationException("User email can not be null"); }
            if (!Regex.IsMatch(
                Value,
                @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
            )) throw new ValidationException("User email is invalid");
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return value;
        }
    }
}
