using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;

namespace DeviceManagement_WebApp.Repositories
{
    public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    {
        private readonly ConnectedOfficeContext _context = new ConnectedOfficeContext();
        public CategoriesRepository(ConnectedOfficeContext context) : base(context)
        {
            _context = context;
        }
    }
}
