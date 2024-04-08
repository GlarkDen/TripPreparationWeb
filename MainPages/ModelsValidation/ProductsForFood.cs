using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class ProductsForFood
{
    public int ProductNumber { get; set; }

    public int FoodNumber { get; set; }

    public int Amount { get; set; }

    public virtual Food FoodNumberNavigation { get; set; } = null!;

    public virtual Product ProductNumberNavigation { get; set; } = null!;
}
