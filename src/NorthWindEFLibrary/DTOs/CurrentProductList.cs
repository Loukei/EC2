using System;
using System.Collections.Generic;

namespace NorthWindEFLibrary.DTOs;

public partial class CurrentProductList
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;
}
