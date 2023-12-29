using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

		
	}
}
