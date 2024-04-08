using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class TripType
{
    public int TripTypeNumber { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<TripsList> TripsLists { get; set; } = new List<TripsList>();
}
