using System;
using System.Collections.Generic;

namespace NorthWindEFLibrary.DTOs;

public partial class ProductsAboveAveragePrice
{
    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }
}
