namespace NGINX.Attribuites
{
    public class AllowedExtension : ValidationAttribute
    {
        private readonly string _AllowedExtension;

        public AllowedExtension(string allowedExtension)
        {
            _AllowedExtension = allowedExtension;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not  null)
            {
                var extension = Path.GetExtension(file.FileName);


                var IsAllowed = _AllowedExtension.Split(',').Contains(extension ,StringComparer.OrdinalIgnoreCase);
                if (!IsAllowed) 
                {
                    return new ValidationResult($"Only {_AllowedExtension} are Supported");
                }
            }
            return ValidationResult.Success;
        }
    }
}
