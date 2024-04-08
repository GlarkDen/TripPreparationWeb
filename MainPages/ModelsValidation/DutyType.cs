using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class DutyType
{
    public int DutyTypeNumber { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Duty> Duties { get; set; } = new List<Duty>();

    public virtual ICollection<User> UserNumbers { get; set; } = new List<User>();
}
