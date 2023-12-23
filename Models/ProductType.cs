using System;
using System.Collections.Generic;

namespace DoAn1_DDG_Pro.Models;

public partial class ProductType
{
    public string TypeId { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
