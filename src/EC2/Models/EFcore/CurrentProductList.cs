using System;
using System.Collections.Generic;

namespace EC2.Models.EFcore;

public partial class CurrentProductList
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;
}
