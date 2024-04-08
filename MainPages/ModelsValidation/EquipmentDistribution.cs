using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class EquipmentDistribution
{
    public int EquipmentNumber { get; set; }

    public int ParticipantNumber { get; set; }

    public int Amount { get; set; }

    public virtual EquipmentList EquipmentNumberNavigation { get; set; } = null!;

    public virtual ParticipantsList ParticipantNumberNavigation { get; set; } = null!;
}
