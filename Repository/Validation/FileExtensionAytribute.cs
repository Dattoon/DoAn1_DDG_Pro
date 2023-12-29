using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace DoAn1_DDG_Pro.Repository.Validation
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName).TrimStart('.').ToLowerInvariant();

                switch (extension)
                {
                    case "jpg":
                    case "png":
                    case "jpeg":
                    case "gif":
                        return ValidationResult.Success;
                    default:
                        return new ValidationResult("Allowed extensions are .jpg, .png, .jpeg, .gif");
                }
            }

            return ValidationResult.Success;
        }
    }
}
