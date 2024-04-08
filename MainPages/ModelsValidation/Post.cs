using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class Post
{
    public int PostNumber { get; set; }

    public string Name { get; set; } = null!;

    public string Responsibilities { get; set; } = null!;

    public virtual ICollection<ParticipantsList> ParticipantNumbers { get; set; } = new List<ParticipantsList>();

    public virtual ICollection<User> UserNumbers { get; set; } = new List<User>();
}
