using System;
using System.Collections.Generic;

namespace EC2.Models.EFCore;

public partial class OrderSubtotal
{
    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
