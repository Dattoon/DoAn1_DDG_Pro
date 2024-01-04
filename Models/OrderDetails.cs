using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DoAn1_DDG_Pro.Models
{
    public class OrderDetails
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string OrderCode { get; set; }

        public int ProductId { get; set; }

        [Column(TypeName = "decimal(18,0)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        // Thêm các trường mới
        public string CustomerName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string PaymentMethod { get; set; }

        [AllowNull]
        public string Description { get; set; } 


        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}