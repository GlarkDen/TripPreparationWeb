using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class Duty
{
    public int DutyNumber { get; set; }

    public int TripNumber { get; set; }

    public int DutyTypeNumber { get; set; }

    public TimeOnly Start { get; set; }

    public DateOnly Date { get; set; }

    public string? Note { get; set; }

    public virtual DutyType DutyTypeNumberNavigation { get; set; } = null!;

    public virtual TripsList TripNumberNavigation { get; set; } = null!;

    public virtual ICollection<ParticipantsList> ParticipantNumbers { get; set; } = new List<ParticipantsList>();
}
