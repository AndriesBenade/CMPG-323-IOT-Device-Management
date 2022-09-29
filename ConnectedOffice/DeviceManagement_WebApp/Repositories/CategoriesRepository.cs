using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DeviceManagement_WebApp.Data;
using System;
using System.Linq;

namespace DeviceManagement_WebApp.Repositories
{
    public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    {
        private readonly ConnectedOfficeContext _context = new ConnectedOfficeContext();
        public CategoriesRepository(ConnectedOfficeContext context) : base(context)
        {
            _context = context;
        }

        // transferred data access operations

        public async Task<IActionResult> Index(Controller controller)
        {
            return controller.View(await _context.Category.ToListAsync());
        }

        public async Task<IActionResult> Details(Guid? id, Controller controller)
        {
            if (id == null)
            {
                return controller.NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return controller.NotFound();
            }

            return controller.View(category);
        }

        // GET: Categories/Create
        public IActionResult Create(Controller controller)
        {
            return controller.View();
        }

        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category, Controller controller)
        {
            category.CategoryId = Guid.NewGuid();
            _context.Add(category);
            await _context.SaveChangesAsync();
            return controller.RedirectToAction(nameof(Index));
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(Guid? id, Controller controller)
        {
            if (id == null)
            {
                return controller.NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return controller.NotFound();
            }
            return controller.View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category, Controller controller)
        {
            if (id != category.CategoryId)
            {
                return controller.NotFound();
            }
            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.CategoryId))
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

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(Guid? id, Controller controller)
        {
            if (id == null)
            {
                return controller.NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return controller.NotFound();
            }

            return controller.View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id, Controller controller)
        {
            var category = await _context.Category.FindAsync(id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return controller.RedirectToAction(nameof(Index));
        }

        public bool CategoryExists(Guid id)
        {
            return _context.Category.Any(e => e.CategoryId == id);
        }
    }
}
