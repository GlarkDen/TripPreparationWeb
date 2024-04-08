using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class BoatType
{
    public int BoatTypeNumber { get; set; }

    public string Name { get; set; } = null!;

    public int PlacesAmount { get; set; }

    public virtual ICollection<BoatsList> BoatsLists { get; set; } = new List<BoatsList>();
}
