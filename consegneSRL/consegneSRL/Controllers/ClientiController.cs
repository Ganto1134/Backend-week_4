using Microsoft.AspNetCore.Mvc;

namespace consegneSRL.Controllers
{
    public class ClientiController : Controller
    {
        private readonly SpedizioniContext _context;

        public ClientiController(SpedizioniContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var clienti = _context.Clienti.ToList();
            return View(clienti);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Clienti.Add(cliente);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        public IActionResult Edit(int id)
        {
            var cliente = _context.Clienti.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        public IActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Clienti.Update(cliente);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        public IActionResult Delete(int id)
        {
            var cliente = _context.Clienti.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var cliente = _context.Clienti.Find(id);
            _context.Clienti.Remove(cliente);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var cliente = _context.Clienti.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }
    }
}