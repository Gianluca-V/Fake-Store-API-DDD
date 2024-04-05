using Domain.Common.Models;
using Domain.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductService
{
    public record ProductResult
    (
      Product product
    );

    public record ProductPagedList(
        PagedList<ProductResult> PagedList
    );
}
