using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Product
{
    public record CreateProductRequest
    (
        [Required] string name,
        [Required] float price,
        [Required] string category,
        string? description,
        List<string>? images
        
    );
}
