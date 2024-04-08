using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class FoodIntakeType
{
    public int FoodIntakeTypeNumber { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();

    public virtual ICollection<User> UserNumbers { get; set; } = new List<User>();
}
