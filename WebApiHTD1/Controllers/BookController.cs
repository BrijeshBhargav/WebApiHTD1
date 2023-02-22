using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiHTD1.Models;

namespace WebApiHTD1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly DataBaseContext _context;
        public BookController(DataBaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<BookModel> Get()
        {
            return _context.BOOK.ToList();
        }
        [HttpGet]
        [Route("Get")]
        public IEnumerable<BookModel> Get(int ID)
        {
            return _context.BOOK.Where(s => s.ID == ID);
        }
        [HttpGet]
        [Route("Details")]
        public IEnumerable<BookModel> Details(int ID)
        {
            return _context.BOOK.Where(s => s.ID == ID);
        }
        [HttpPost]
        [Route("InsertData")]
        public bool InsertData(BookModel obj)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }
            else
            {
                _context.BOOK.Add(obj);
                _context.SaveChanges();
                return true;
            }
        }
        [Route("UpdateData")]
        public bool UpdateData(BookModel obj)
        {
            if (ModelState.IsValid)
            {
                _context.Update(obj);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        [Route("DeleteData")]
        public bool DeleteData(int ID)
        {
            var result = _context.BOOK.Find(ID);
            if (result != null)
            {
                _context.BOOK.Remove(result);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
