using System;
using System.Collections.Generic;

namespace EC2.Models.EFcore;

public partial class Shipper
{
    public int ShipperId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? Phone { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
