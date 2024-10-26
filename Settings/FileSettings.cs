namespace NGINX.Settings
{
    public static class FileSettings
    {
        public const string ImagesPath = "/Assets/Images/Games";
        public const string AllowedExtensions = ".jpg,.png";
        public const int MaxFileSizeInMB = 1 ;
        public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024 ;

    }
}
