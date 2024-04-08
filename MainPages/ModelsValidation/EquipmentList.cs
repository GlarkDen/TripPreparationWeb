using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class EquipmentList
{
    public int EquipmentNumber { get; set; }

    public int TripNumber { get; set; }

    public string Name { get; set; } = null!;

    public int Amount { get; set; }

    public string? Owner { get; set; }

    public double? Weight { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<EquipmentDistribution> EquipmentDistributions { get; set; } = new List<EquipmentDistribution>();

    public virtual TripsList TripNumberNavigation { get; set; } = null!;
}
