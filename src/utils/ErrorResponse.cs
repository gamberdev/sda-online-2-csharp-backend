using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.utils;

public class ErrorResponse
{
    public bool Success { get; set; } = false;
    public string? Message { get; set; }
}
