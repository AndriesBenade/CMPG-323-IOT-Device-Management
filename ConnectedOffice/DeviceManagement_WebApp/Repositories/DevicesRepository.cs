using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace DeviceManagement_WebApp.Repositories
{
    public class DevicesRepository : GenericRepository<Device>, IDevicesRepository
    {
        private readonly ConnectedOfficeContext _context = new ConnectedOfficeContext();
        public DevicesRepository(ConnectedOfficeContext context) : base(context)
        {
            _context = context;
        }

        // transferred data access operations

        // GET: Devices
        public async Task<IActionResult> Index(Controller controller)
        {
            var connectedOfficeContext = _context.Device.Include(d => d.Category).Include(d => d.Zone);
            return controller.View(await connectedOfficeContext.ToListAsync());
        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(Guid? id, Controller controller)
        {
            if (id == null)
            {
                return controller.NotFound();
            }

            var device = await _context.Device
                .Include(d => d.Category)
                .Include(d => d.Zone)
                .FirstOrDefaultAsync(m => m.DeviceId == id);
            if (device == null)
            {
                return controller.NotFound();
            }

            return controller.View(device);
        }

        // GET: Devices/Create
        public IActionResult Create(Controller controller)
        {
            controller.ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            controller.ViewData["ZoneId"] = new SelectList(_context.Zone, "ZoneId", "ZoneName");
            return controller.View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device, Controller controller)
        {
            device.DeviceId = Guid.NewGuid();
            _context.Add(device);
            await _context.SaveChangesAsync();
            return controller.RedirectToAction(nameof(Index));


        }

        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(Guid? id, Controller controller)
        {
            if (id == null)
            {
                return controller.NotFound();
            }

            var device = await _context.Device.FindAsync(id);
            if (device == null)
            {
                return controller.NotFound();
            }
            controller.ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", device.CategoryId);
            controller.ViewData["ZoneId"] = new SelectList(_context.Zone, "ZoneId", "ZoneName", device.ZoneId);
            return controller.View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device, Controller controller)
        {
            if (id != device.DeviceId)
            {
                return controller.NotFound();
            }
            try
            {
                _context.Update(device);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(device.DeviceId))
                {
                    return controller.NotFound();
                }
                else
                {
                    throw;
                }
            }
            return controller.RedirectToAction(nameof(Index));

        }

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(Guid? id, Controller controller)
        {
            if (id == null)
            {
                return controller.NotFound();
            }

            var device = await _context.Device
                .Include(d => d.Category)
                .Include(d => d.Zone)
                .FirstOrDefaultAsync(m => m.DeviceId == id);
            if (device == null)
            {
                return controller.NotFound();
            }

            return controller.View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id, Controller controller)
        {
            var device = await _context.Device.FindAsync(id);
            _context.Device.Remove(device);
            await _context.SaveChangesAsync();
            return controller.RedirectToAction(nameof(Index));
        }

        public bool DeviceExists(Guid id)
        {
            return _context.Device.Any(e => e.DeviceId == id);
        }
    }
}
