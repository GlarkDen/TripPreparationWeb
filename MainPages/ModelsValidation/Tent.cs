using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class Tent
{
    public int TentNumber { get; set; }

    public int TripNumber { get; set; }

    public int PlacesAmount { get; set; }

    public virtual ICollection<ParticipantsList> ParticipantsLists { get; set; } = new List<ParticipantsList>();

    public virtual TripsList TripNumberNavigation { get; set; } = null!;
}
