using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopifyBackend.Models;
using ShopifyBackend.Models.ViewModels;

namespace ShopifyBackend.Controllers
{
    public class InventoriesController : Controller
    {
        private readonly InventoryContext _context;

        public InventoriesController(InventoryContext context)
        {
            _context = context;
        }

        // GET: Inventories
        public async Task<IActionResult> Index()
        {
            if(_context.Inventories != null)
            {
                List<Location> locations = await _context.Locations.ToListAsync();
                List <Inventory> inventories = await _context.Inventories.ToListAsync();
                List<Relationship> relationship = await _context.Relationships.ToListAsync();
                inventories.ForEach(i =>
                {
                    var r = relationship.Where(r => r.InventoryId == i.Id).FirstOrDefault();
                    if(r != null)
                        i.LocationName = locations.Where(l => l.Id == r.LocationId).FirstOrDefault().Name;
                    else
                    {
                        i.LocationName = "Undefined";
                    }
                });
                return View(inventories);
            }
            return Problem("Entity set 'InventoryContext.Inventories'  is null.");

        }

        public async Task<IActionResult> EditLocation(int? ID)
        {
            InventoryLocationViewModel viewModel = new InventoryLocationViewModel();
            if (ID != null)
            {
                viewModel.Inventory = await _context.Inventories.Where(i => i.Id == ID).FirstOrDefaultAsync();
                viewModel.Locations = await _context.Locations.ToListAsync();
                
                Relationship r = await _context.Relationships.Where(r => r.InventoryId == ID).FirstOrDefaultAsync();
                if(r != null)
                    viewModel.Location = viewModel.Locations.Where(l => l.Id == r.LocationId).FirstOrDefault();
                else
                    viewModel.Location = null;
            }
            return View(viewModel);
        }

        public async Task<IActionResult> AddLocation(int? lID, int? iID)
        {
            if (lID != null && iID != null)
            {
                var r1 = await _context.Relationships.Where(i => i.InventoryId == iID).FirstOrDefaultAsync();
                if(r1 != null)
                    _context.Relationships.Remove(r1);

                Relationship r = new Relationship
                {
                    LocationId = (int)lID,
                    InventoryId = (int)iID,
                };
               
                _context.Add(r);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("EditLocation", new { ID = iID });

        }



        public async Task<IActionResult> RemoveLocation(int? lID, int? iID)
        {
            if (lID != null && iID != null)
            {
                var r = await _context.Relationships.Where(i => i.InventoryId == iID ).FirstOrDefaultAsync();
                _context.Relationships.Remove(r);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("EditLocation", new { ID = iID });

        }





        // GET: Inventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Inventories == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Inventories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Quantity")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventory);
        }

        // GET: Inventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Inventories == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Quantity")] Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Inventories == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Inventories == null)
            {
                return Problem("Entity set 'InventoryContext.Inventories'  is null.");
            }
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory != null)
            {
                _context.Inventories.Remove(inventory);
                var r = await _context.Relationships.FirstOrDefaultAsync(r => r.InventoryId == id);
                if(r!=null)
                    _context.Relationships.Remove(r);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryExists(int id)
        {
          return (_context.Inventories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
