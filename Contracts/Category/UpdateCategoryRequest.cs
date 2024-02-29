﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Product
{
    public record UpdateCategoryRequest
    (
        [Required] string name,
        string? image
    );
}
