using System.ComponentModel.DataAnnotations;

namespace Shopthoitrang.Data.Valication
{
    public class FileExtensionAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is IFormFile file)
            {
                var imgType = Path.GetExtension(file.FileName);
                string[] imgTypes = { "jpg", "png", "jpeg" };
                bool kq = imgTypes.Any(x => imgType.EndsWith(x));
                if (!kq)
                {
                    return new ValidationResult("chỉ chọn ảnh jpg,png");
                }
                
            }
            return ValidationResult.Success;
        }
    }
}
