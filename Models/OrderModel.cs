using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn1_DDG_Pro.Models
{
	public class OrderModel
	{
		[Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

		public string OrderCode { get; set; }

		public string UserName { get; set; }

		public DateTime CreatedDate { get; set; }

		public int Status { get; set; }
       

    }
}
