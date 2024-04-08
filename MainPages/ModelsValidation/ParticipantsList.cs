using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class ParticipantsList
{
    public int ParticipantNumber { get; set; }

    public int UserNumber { get; set; }

    public int TripNumber { get; set; }

    public int? TentNumber { get; set; }

    public int? BoatNumber { get; set; }

    public virtual BoatsList? BoatNumberNavigation { get; set; }

    public virtual ICollection<EquipmentDistribution> EquipmentDistributions { get; set; } = new List<EquipmentDistribution>();

    public virtual Tent? TentNumberNavigation { get; set; }

    public virtual TripsList TripNumberNavigation { get; set; } = null!;

    public virtual User UserNumberNavigation { get; set; } = null!;

    public virtual ICollection<Duty> DutyNumbers { get; set; } = new List<Duty>();

    public virtual ICollection<Post> PostNumbers { get; set; } = new List<Post>();
}
