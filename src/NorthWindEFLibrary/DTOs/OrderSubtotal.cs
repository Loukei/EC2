using System;
using System.Collections.Generic;

namespace NorthWindLibrary.DTOs;

public partial class OrderSubtotal
{
    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
