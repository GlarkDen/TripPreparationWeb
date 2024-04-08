using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class Route
{
    public int PointNumber { get; set; }

    public int TripNumber { get; set; }

    public string? Description { get; set; }

    public virtual WaypointList PointNumberNavigation { get; set; } = null!;

    public virtual TripsList TripNumberNavigation { get; set; } = null!;
}
