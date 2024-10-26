
using Microsoft.EntityFrameworkCore;

namespace NGINX.Services
{
    public class DevicesServices : IDevicesServices
    {
        private readonly ApplicationDbContext _Context;

        public DevicesServices(ApplicationDbContext context)
        {
            _Context = context;
        }

        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _Context.Devices
                .Select(d => new SelectListItem() { Value = d.Id.ToString(), Text = d.Name })
                .OrderBy(d => d.Text)
                .AsNoTracking()
                .ToList();
        }
    }
}
