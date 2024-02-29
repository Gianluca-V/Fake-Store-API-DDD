using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Models
{
    public abstract class AggregateRoot<TId> : Entity<TId> where TId : class
    {
        protected AggregateRoot(TId Id) : base(Id)
        {
        }
        protected AggregateRoot() { }
    }
}
