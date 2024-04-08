using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class Food
{
    public int FoodNumber { get; set; }

    public int TripNumber { get; set; }

    public int FoodIntakeTypeNumber { get; set; }

    public DateOnly Date { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<DishesForFood> DishesForFoods { get; set; } = new List<DishesForFood>();

    public virtual FoodIntakeType FoodIntakeTypeNumberNavigation { get; set; } = null!;

    public virtual ICollection<ProductsForFood> ProductsForFoods { get; set; } = new List<ProductsForFood>();

    public virtual TripsList TripNumberNavigation { get; set; } = null!;
}
