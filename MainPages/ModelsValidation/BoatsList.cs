using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class BoatsList
{
    public int BoatNumber { get; set; }

    public int TripNumber { get; set; }

    public int BoatTypeNumber { get; set; }

    public virtual BoatType BoatTypeNumberNavigation { get; set; } = null!;

    public virtual ICollection<ParticipantsList> ParticipantsLists { get; set; } = new List<ParticipantsList>();

    public virtual TripsList TripNumberNavigation { get; set; } = null!;
}
