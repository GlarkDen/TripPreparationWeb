using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class Cost
{
    public string CostName { get; set; } = null!;

    public int TripNumber { get; set; }

    public double Money { get; set; }

    public string WhoSpent { get; set; } = null!;

    public string? Note { get; set; }

    public virtual TripsList TripNumberNavigation { get; set; } = null!;
}
