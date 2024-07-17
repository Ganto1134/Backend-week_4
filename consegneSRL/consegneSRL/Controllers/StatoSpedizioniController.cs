using Microsoft.AspNetCore.Mvc;

namespace consegneSRL.Controllers
{
    public class StatoSpedizioniController : Controller
    {
        private readonly SpedizioniContext _context;

        public StatoSpedizioniController(SpedizioniContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int spedizioneId)
        {
            var stati = await _context.StatiSpedizione
                .Where(s => s.SpedizioneId == spedizioneId)
                .OrderByDescending(s => s.DataOra)
                .ToListAsync();
            ViewBag.SpedizioneId = spedizioneId;
            return View(stati);
        }

        public IActionResult Create(int spedizioneId)
        {
            ViewBag.SpedizioneId = spedizioneId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StatoSpedizione statoSpedizione)
        {
            if (ModelState.IsValid)
            {
                _context.StatiSpedizione.Add(statoSpedizione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { spedizioneId = statoSpedizione.SpedizioneId });
            }
            ViewBag.SpedizioneId = statoSpedizione.SpedizioneId;
            return View(statoSpedizione);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var stato = await _context.StatiSpedizione.FindAsync(id);
            if (stato == null)
            {
                return NotFound();
            }
            ViewBag.SpedizioneId = stato.SpedizioneId;
            return View(stato);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StatoSpedizione statoSpedizione)
        {
            if (ModelState.IsValid)
            {
                _context.StatiSpedizione.Update(statoSpedizione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { spedizioneId = statoSpedizione.SpedizioneId });
            }
            ViewBag.SpedizioneId = statoSpedizione.SpedizioneId;
            return View(statoSpedizione);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var stato = await _context.StatiSpedizione.FindAsync(id);
            if (stato == null)
            {
                return NotFound();
            }
            return View(stato);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stato = await _context.StatiSpedizione.FindAsync(id);
            _context.StatiSpedizione.Remove(stato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { spedizioneId = stato.SpedizioneId });
        }
    }
}
