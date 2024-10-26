namespace NGINX.ViewModels
{
    public class CreateGameFormViewModel : GameFormViewModel
    {
       [AllowedExtension(FileSettings.AllowedExtensions)]
        [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default;
    }
}
