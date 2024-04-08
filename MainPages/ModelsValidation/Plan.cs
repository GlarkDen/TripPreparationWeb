using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class Plan
{
    public int EventNumber { get; set; }

    public int TripNumber { get; set; }

    public string Name { get; set; } = null!;

    public TimeOnly Start { get; set; }

    public TimeOnly End { get; set; }

    public DateOnly Date { get; set; }

    public string? Description { get; set; }

    public virtual TripsList TripNumberNavigation { get; set; } = null!;
}
