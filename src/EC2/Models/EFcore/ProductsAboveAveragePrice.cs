using System;
using System.Collections.Generic;

namespace EC2.Models.EFcore;

public partial class ProductsAboveAveragePrice
{
    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }
}
