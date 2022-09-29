using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace DeviceManagement_WebApp.Repositories
{
    public class ZonesRepository : GenericRepository<Zone>, IZonesRepository
    {
        private readonly ConnectedOfficeContext _context = new ConnectedOfficeContext();
        public ZonesRepository(ConnectedOfficeContext context) : base(context)
        {
            _context = context;
        }

        // transferred data access operations

        // GET: Zones
        public async Task<IActionResult> Index(Controller controller)
        {
            return controller.View(await _context.Zone.ToListAsync());
        }

        // GET: Zones/Details/5
        public async Task<IActionResult> Details(Guid? id, Controller controller)
        {
            if (id == null)
            {
                return controller.NotFound();
            }

            var zone = await _context.Zone
                .FirstOrDefaultAsync(m => m.ZoneId == id);
            if (zone == null)
            {
                return controller.NotFound();
            }

            return controller.View(zone);
        }

        // GET: Zones/Create
        public IActionResult Create(Controller controller)
        {
            return controller.View();
        }

        // POST: Zones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone, Controller controller)
        {
            zone.ZoneId = Guid.NewGuid();
            _context.Add(zone);
            await _context.SaveChangesAsync();

            return controller.RedirectToAction(nameof(Index));
        }

        // GET: Zones/Edit/5
        public async Task<IActionResult> Edit(Guid? id, Controller controller)
        {
            if (id == null)
            {
                return controller.NotFound();
            }

            var zone = await _context.Zone.FindAsync(id);
            if (zone == null)
            {
                return controller.NotFound();
            }
            return controller.View(zone);
        }

        // POST: Zones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone, Controller controller)
        {
            if (id != zone.ZoneId)
            {
                return controller.NotFound();
            }

            try
            {
                _context.Update(zone);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneExists(zone.ZoneId))
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

        // GET: Zones/Delete/5
        public async Task<IActionResult> Delete(Guid? id, Controller controller)
        {
            if (id == null)
            {
                return controller.NotFound();
            }

            var zone = await _context.Zone
                .FirstOrDefaultAsync(m => m.ZoneId == id);
            if (zone == null)
            {
                return controller.NotFound();
            }

            return controller.View(zone);
        }

        // POST: Zones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id, Controller controller)
        {
            var zone = await _context.Zone.FindAsync(id);
            _context.Zone.Remove(zone);
            await _context.SaveChangesAsync();
            return controller.RedirectToAction(nameof(Index));
        }
        
        public bool ZoneExists(Guid id)
        {
            return _context.Zone.Any(e => e.ZoneId == id);
        }
    }
}
