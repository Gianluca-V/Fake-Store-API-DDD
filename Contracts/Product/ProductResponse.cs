using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Product
{
    public record ProductPaginatedResponse(
      List<ProductResponse> ProductList,
      PagedList PaginationInfo
    );
    public record ProductResponse
    (
        Guid Id,
        string Name,
        string Description,
        float Price,
        List<string> Images,
        ProductCategoryResponse Category
    );
    public record ProductCategoryResponse
    {
        public Guid CategoryId { get; init; }
        public string Name { get; init; }
        public string Image { get; init; }
    }
    public record PagedList {
        public int Page { get; init; }
        public int PageSize { get; init; }
        public int TotalItemCount { get; init; }
        public int TotalPageCount { get; init; }
        public bool HasNextPage => Page * PageSize < TotalItemCount;
        public bool HasPrevPage => Page > 1;
    }

}
