﻿using System;
using System.Collections.Generic;

namespace EC2.Models.DTOs.Northwind;

public partial class CategorySalesFor1997
{
    public string CategoryName { get; set; } = null!;

    public decimal? CategorySales { get; set; }
}