using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class DishesForFood
{
    public string DishName { get; set; } = null!;

    public int FoodNumber { get; set; }

    public virtual Food FoodNumberNavigation { get; set; } = null!;
}
