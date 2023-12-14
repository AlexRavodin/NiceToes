using System.ComponentModel.DataAnnotations;

namespace NiceToes.Web.Utility
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not IFormFile file) return ValidationResult.Success;

            return file.Length > (_maxFileSize * 2048 * 2048) ? new ValidationResult($"Maximum allowed file size is {_maxFileSize} MB.") : ValidationResult.Success;
        }

    }
}
