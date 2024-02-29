using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Category
{
    public record CreateCategoryRequest
    (
        [Required] string name,
        string? image
    ); 
}
