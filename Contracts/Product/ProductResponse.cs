using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Product
{
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

}
