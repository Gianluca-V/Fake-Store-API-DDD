using Domain.CategoryAggregate;
using Domain.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CategoryService
{
    public record CategoryResult
    (
      Category category
    );
}
