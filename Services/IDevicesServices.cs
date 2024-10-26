namespace NGINX.Services
{
    public interface IDevicesServices
    {
        IEnumerable<SelectListItem> GetSelectList();
    }
}
