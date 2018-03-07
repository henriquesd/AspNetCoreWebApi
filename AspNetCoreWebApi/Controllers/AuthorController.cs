using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Book")]
    public class BookController : Controller
    {
        private readonly AspNetCoreWebApiContext _context;

        public BookController(AspNetCoreWebApiContext context)
        {
            _context = context;

            if (_context.Books.Count() == 0)
            {
                _context.Books.Add(new Book { Title = "The Peril of the Good Side of the Soul", Category = "Christian", Author = "Dong Yu Lan" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult GetById(long id)
        {
            var book = _context.Books.FirstOrDefault(t => t.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return new ObjectResult(book);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            _context.Books.Add(book);
            _context.SaveChanges();

            return CreatedAtRoute("GetBook", new { id = book.Id }, book);
        }

        //PUT needs to send the complete entity; to partial update, use HTTP PATCH;
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Book item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var book = _context.Books.FirstOrDefault(t => t.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            book.Title = item.Title;
            book.Category = item.Category;
            book.Author = item.Author;

            _context.Books.Update(book);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var book = _context.Books.FirstOrDefault(t => t.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
            return new NoContentResult();
        }

    }
}