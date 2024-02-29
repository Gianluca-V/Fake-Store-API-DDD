using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Category
{
    public record CategoryResponse
    (
     Guid Id,
     string Name,
     string Image
    );
}
