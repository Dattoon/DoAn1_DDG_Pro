using System.ComponentModel.DataAnnotations;

namespace DoAn1_DDG_Pro.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Bạn hãy nhập UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bạn hãy nhập Email"), EmailAddress]
        public string Email { get; set; }


        public DateTime DateOfBirth { get; set; }



        [DataType(DataType.Password), Required(ErrorMessage = "Bạn hãy nhập Password")]
        public string Password { get; set; }
    }
}
