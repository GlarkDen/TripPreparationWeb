using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class RepairKit
{
    public string ToolName { get; set; } = null!;

    public int TripNumber { get; set; }

    public int Amount { get; set; }

    public string? Note { get; set; }

    public virtual TripsList TripNumberNavigation { get; set; } = null!;
}
