using System;
using System.Collections.Generic;

namespace MainPages.Models;

public partial class Product
{
    public int ProductNumber { get; set; }

    public string ProductName { get; set; } = null!;

    public int? Weight { get; set; }

    public int? Kcal { get; set; }

    public int? Proteins { get; set; }

    public int? Fats { get; set; }

    public int? Carbohydrates { get; set; }

    public virtual ICollection<ProductsForFood> ProductsForFoods { get; set; } = new List<ProductsForFood>();

    public virtual ICollection<ProductsList> ProductsLists { get; set; } = new List<ProductsList>();
}
