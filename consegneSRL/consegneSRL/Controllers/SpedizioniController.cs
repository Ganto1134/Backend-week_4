using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using consegneSRL.Data;
using consegneSRL.Models;

namespace consegneSRL.Controllers
{
    public class SpedizioniController : Controller
    {
        private readonly SpedizioniContext _context;

        public SpedizioniController(SpedizioniContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var spedizioni = await _context.Spedizioni.Include(s => s.Cliente).ToListAsync();
            return View(spedizioni);
        }

        public IActionResult Create()
        {
            ViewBag.Clienti = _context.Clienti.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Spedizione spedizione)
        {
            if (ModelState.IsValid)
            {
                _context.Spedizioni.Add(spedizione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Clienti = _context.Clienti.ToList();
            return View(spedizione);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var spedizione = await _context.Spedizioni.FindAsync(id);
            if (spedizione == null)
            {
                return NotFound();
            }
            ViewBag.Clienti = _context.Clienti.ToList();
            return View(spedizione);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Spedizione spedizione)
        {
            if (ModelState.IsValid)
            {
                _context.Spedizioni.Update(spedizione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Clienti = _context.Clienti.ToList();
            return View(spedizione);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var spedizione = await _context.Spedizioni.Include(s => s.Cliente).FirstOrDefaultAsync(m => m.Id == id);
            if (spedizione == null)
            {
                return NotFound();
            }
            return View(spedizione);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spedizione = await _context.Spedizioni.FindAsync(id);
            _context.Spedizioni.Remove(spedizione);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var spedizione = await _context.Spedizioni.Include(s => s.Cliente).FirstOrDefaultAsync(m => m.Id == id);
            if (spedizione == null)
            {
                return NotFound();
            }
            return View(spedizione);
        }
    }
}
