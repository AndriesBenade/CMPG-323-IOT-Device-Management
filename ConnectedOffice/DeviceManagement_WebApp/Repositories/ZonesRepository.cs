using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;

namespace DeviceManagement_WebApp.Repositories
{
    public class ZonesRepository : GenericRepository<Zone>, IZonesRepository
    {
        private readonly ConnectedOfficeContext _context = new ConnectedOfficeContext();
        public ZonesRepository(ConnectedOfficeContext context) : base(context)
        {
            _context = context;
        }
    }
}
