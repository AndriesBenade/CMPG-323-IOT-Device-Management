using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;

namespace DeviceManagement_WebApp.Repositories
{
    public class DevicesRepository : GenericRepository<Device>, IDevicesRepository
    {
        private readonly ConnectedOfficeContext _context = new ConnectedOfficeContext();
        public DevicesRepository(ConnectedOfficeContext context) : base(context)
        {
            _context = context;
        }
    }
}
