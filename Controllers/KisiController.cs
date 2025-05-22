using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelefonRehberi.Data;
using TelefonRehberi.Models;


namespace TelefonRehberi.Controllers
{
    public class KisiController : Controller
    {
        private readonly AppDbContext _context;

        public KisiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Kisi
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kisiler.ToListAsync());
        }
        
        // GET: Kisi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kisiler == null)
            {
                return NotFound();
            }

            var kisi = await _context.Kisiler
                .FirstOrDefaultAsync(m => m.KisiId == id);
            if (kisi == null)
            {
                return NotFound();
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_DetailsPartial", kisi);
            }
            return View(kisi);
        }
        
        // GET: Kisi/Create
        public IActionResult Create()
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_CreatePartial");
            }
            return View();
        }
        
        // POST: Kisi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KisiId,Ad,Soyad,TelefonNumarasi")] Kisi kisi)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(kisi);
                    await _context.SaveChangesAsync();
                    
                    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        return Json(new { success = true });
                    }
                    return RedirectToAction(nameof(Index));
                }
                    catch (DbUpdateException ex)
                {
                    // Unique constraint violation handling
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("Duplicate"))
                    {
                        if (ex.InnerException.Message.Contains("IX_Kisiler_TelefonNumarasi"))
                        {
                            // Only add to the specific field, not to the summary
                            ModelState.AddModelError("TelefonNumarasi", "Bu telefon numarası zaten kayıtlı.");
                        }
                        else if (ex.InnerException.Message.Contains("IX_Kisiler_Ad_Soyad"))
                        {
                            // Add to model state without field name for Ad-Soyad combination
                            ModelState.AddModelError("", "Bu isim ve soyisim kombinasyonu zaten kayıtlı.");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Kayıt sırasında bir hata oluştu.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kayıt sırasında bir hata oluştu. Lütfen tekrar deneyin.");
                    }
                }
            }
            
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_CreatePartial", kisi);
            }
            return View(kisi);        }        // GET: Kisi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kisiler == null)
            {
                return NotFound();
            }

            var kisi = await _context.Kisiler.FindAsync(id);
            if (kisi == null)
            {
                return NotFound();
            }
            
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_EditPartial", kisi);
            }
            return View(kisi);
        }        
        
        // POST: Kisi/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KisiId,Ad,Soyad,TelefonNumarasi")] Kisi kisi)
        {
            if (id != kisi.KisiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kisi);
                    await _context.SaveChangesAsync();
                    
                    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        return Json(new { success = true });
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KisiExists(kisi.KisiId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException ex)
                {
                    // Unique constraint violation handling
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("Duplicate"))
                    {
                        if (ex.InnerException.Message.Contains("IX_Kisiler_TelefonNumarasi"))
                        {
                            ModelState.AddModelError("TelefonNumarasi", "Bu telefon numarası zaten başka bir kişiye kayıtlı.");
                        }
                        else if (ex.InnerException.Message.Contains("IX_Kisiler_Ad_Soyad"))
                        {
                            ModelState.AddModelError("", "Bu isim ve soyisim kombinasyonu zaten başka bir kişi için kayıtlı.");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu. Lütfen tekrar deneyin.");
                    }
                }
            }
            
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_EditPartial", kisi);
            }            return View(kisi);
        }
        
        // GET: Kisi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kisiler == null)
            {
                return NotFound();
            }

            var kisi = await _context.Kisiler
                .FirstOrDefaultAsync(m => m.KisiId == id);
            if (kisi == null)
            {
                return NotFound();
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_DeletePartial", kisi);
            }
            return View(kisi);
        }
        
        // POST: Kisi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kisiler == null)
            {
                return Problem("Entity set 'AppDbContext.Kisiler' is null.");
            }
            var kisi = await _context.Kisiler.FindAsync(id);
            if (kisi != null)
            {
                _context.Kisiler.Remove(kisi);
            }
            
            await _context.SaveChangesAsync();
            
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true });
            }
            return RedirectToAction(nameof(Index));
        }

        private bool KisiExists(int id)
        {
            return (_context.Kisiler?.Any(e => e.KisiId == id)).GetValueOrDefault();
        }
    }
}