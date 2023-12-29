using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn1_DDG_Pro.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }



	[Column(TypeName = "decimal(18,0)")]
	public decimal Price { get; set; }

	public int? Quantity { get; set; }

    public string? Description { get; set; }

    public string? TypeId { get; set; }

    public string? Imgtop { get; set; }

    public string? Imgbot { get; set; }

    public virtual ProductType? Type { get; set; }
}
