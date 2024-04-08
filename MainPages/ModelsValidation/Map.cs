using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class Map
{
    public int MapNumber { get; set; }

    public int TripNumber { get; set; }

    public byte[] Map1 { get; set; } = null!;

    public virtual TripsList TripNumberNavigation { get; set; } = null!;
}
