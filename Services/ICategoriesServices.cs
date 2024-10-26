
namespace NGINX.Services
{
    public interface ICategoriesServices
    {
        IEnumerable<SelectListItem> GetSelectList();
    }
}
