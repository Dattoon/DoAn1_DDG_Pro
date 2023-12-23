using System;
using System.Collections.Generic;

namespace DoAn1_DDG_Pro.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public int? Price { get; set; }

    public string? TypeId { get; set; }

    public string? Imgtop { get; set; }

    public string? Imgbot { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ProductType? Type { get; set; }
}
