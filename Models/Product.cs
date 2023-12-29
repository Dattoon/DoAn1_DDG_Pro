using System;
using System.Collections.Generic;

namespace DoAn1_DDG_Pro.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public string? Description { get; set; }

    public string? TypeId { get; set; }

    public string? Imgtop { get; set; }

    public string? Imgbot { get; set; }

    public virtual ProductType? Type { get; set; }
}
