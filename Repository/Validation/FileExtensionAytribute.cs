using System.ComponentModel.DataAnnotations;

namespace DoAn1_DDG_Pro.Repository.Validation
{
    public class FileExtensionAytribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
          if(value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                string[] extensions = { "jpg", "png", "jpeg" };

                bool result = extension.Any(x => extension.EndsWith(x));

                if(!result)
                {
                    return new ValidationResult("Allowed extension are jpg or png or jpeg");
                }

            }
          return ValidationResult.Success;
        }
    }
}
