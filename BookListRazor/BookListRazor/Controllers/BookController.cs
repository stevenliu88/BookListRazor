using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Controllers
{
    [Route("api/Book")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            return Json(new { data = await _db.Book.ToListAsync()});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) 
        {
            var book = await _db.Book.FirstOrDefaultAsync(book => book.Id == id);
            if (book == null) {
                return Json(new { success = false, message = "error occurred while deleting" });
            }
            _db.Remove(book);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
    }
}
