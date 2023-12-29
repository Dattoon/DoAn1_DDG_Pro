using System.ComponentModel.DataAnnotations;

namespace DoAn1_DDG_Pro.Models.ViewModels
{
    public class LoginViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Bạn hãy nhập UserName")]
        public string UserName { get; set; }


        [DataType(DataType.Password), Required(ErrorMessage = "Bạn hãy nhập Password")]
        public string Password { get; set; }


        public string returnUrl { get; set; }
    }
}
