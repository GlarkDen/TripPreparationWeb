using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class ProductsList
{
    public int ProductNumber { get; set; }

    public int TripNumber { get; set; }

    public int Amount { get; set; }

    public string? Note { get; set; }

    public virtual Product ProductNumberNavigation { get; set; } = null!;

    public virtual TripsList TripNumberNavigation { get; set; } = null!;
}
