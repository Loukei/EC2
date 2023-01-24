using System;
using System.Collections.Generic;

namespace EC2.Models.EFCore;

public partial class ProductsAboveAveragePrice
{
    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }
}
