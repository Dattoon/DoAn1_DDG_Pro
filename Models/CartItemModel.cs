
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Numerics;


namespace DoAn1_DDG_Pro.Models
{
    public class CartItemModel
    {
        public int ProductId { get; set; }
        public int Products { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total
        {
            get { return Quantity * Price; }
        }
        public string Imgtop { get; set; }
        public CartItemModel() 
        {
        
        }
        public CartItemModel(Product product)
        {
            Products = product.ProductId;
            ProductName = product.ProductName;
            Price = (decimal)product.Price;
            Quantity = 1;
            Imgtop = product.Imgtop;

        }
    }
}
