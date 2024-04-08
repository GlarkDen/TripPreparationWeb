using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class FirstAidKit
{
    public string MedicineName { get; set; } = null!;

    public int TripNumber { get; set; }

    public int Amount { get; set; }

    public string Purpose { get; set; } = null!;

    public string? Note { get; set; }

    public virtual TripsList TripNumberNavigation { get; set; } = null!;
}
