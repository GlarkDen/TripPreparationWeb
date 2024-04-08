using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class WaypointList
{
    public int PointNumber { get; set; }

    public string Name { get; set; } = null!;

    public string Coordinates { get; set; } = null!;

    public virtual ICollection<Route> Routes { get; set; } = new List<Route>();
}
