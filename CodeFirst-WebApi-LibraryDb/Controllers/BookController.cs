using CodeFirst_WebApi_LibraryDb.DTOs.BookDTOs;
using CodeFirst_WebApi_LibraryDb.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst_WebApi_LibraryDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        LibraryDbContext db;

        public BookController(LibraryDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("GetAllBooks")]
        public ActionResult<IEnumerable<BookDTO>> GetAllBooks()
        {
            List<BookDTO> bookDTOs = db.Books.Select(x => new BookDTO
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return Ok(bookDTOs);
        }
        [HttpGet]
        [Route("GetBookDetail")]
        public ActionResult<BookDTO> GetBook(int id)
        {
            Book book = db.Books.FirstOrDefault(x => x.Id == id);

            if (book == null) return NotFound();

            BookDTO bookDTO = new BookDTO()
            {
                Id = book.Id,
                Name = book.Name
            };
            AuthorBook authorBook = db.AuthorBooks.Where(x => x.BookId == book.Id).FirstOrDefault();
            bookDTO.Author = (db.Authors.FirstOrDefault(x => x.Id == authorBook.AuthorId).FirstName);
            return Ok(bookDTO);
        }

        [HttpPost]
        [Route("CreateBook")]
        public ActionResult<BookCreateDTO> CreateBook(BookCreateDTO bookCreateDTO)
        {
            if (bookCreateDTO == null) return NotFound();

            try
            {
                Author author = db.Authors.FirstOrDefault(x => x.FirstName == bookCreateDTO.AuthorName);
                Book book = new Book()
                {
                    Name = bookCreateDTO.Name
                };
                ExistOrCreateBookType(bookCreateDTO, book);
                string msg = ExistAuthor(author, book);
                if (msg != null) return NotFound(msg);

                db.Books.Add(book);
                db.SaveChanges();

                return CreatedAtAction("GetBook", new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("UpdateBook")]
        public ActionResult<BookDTO> UpdateBook(BookDTO bookDTO)
        {
            Book book = db.Books.FirstOrDefault(x => x.Id == bookDTO.Id);
            if (book == null) return NotFound();

            try
            {
                book.Name = bookDTO.Name;
                db.SaveChanges();
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteBook")]
        public IActionResult DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null) return NotFound();
            AuthorBook authorBook = db.AuthorBooks.FirstOrDefault(x => x.BookId == book.Id);
            List<BookType> bookTypes = db.BookTypes.Where(x => x.BookId == book.Id).ToList();

            try
            {
                db.AuthorBooks.Remove(authorBook);
                foreach (BookType bookType in bookTypes) { db.BookTypes.Remove(bookType); }
                db.Books.Remove(book);
                db.SaveChanges();
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private string ExistAuthor(Author? author, Book book)
        {
            if (author == null)
            {
                return "Önce yazar bilgilerini girmelisiniz.";
            }
            AuthorBook authorBook = new AuthorBook()
            {
                Book = book,
                Author = author
            };
            author.AuthorBooks.Add(authorBook);
            book.AuthorBooks.Add(authorBook);
            db.AuthorBooks.Add(authorBook);

            return null;
        }

        private void ExistOrCreateBookType(BookCreateDTO bookCreateDTO, Book book)
        {
            List<Entities.Type> types = db.Types.Where(x => bookCreateDTO.Types.Contains(x.Name)).ToList();
            List<string> typeName = db.Types.Select(x => x.Name).ToList();
            List<string> names = bookCreateDTO.Types.Where(x => !typeName.Contains(x)).ToList();

            foreach (var item in names)
            {
                Entities.Type type = new Entities.Type()
                {
                    Name = item
                };

                types.Add(type);

                db.Types.Add(type);
            }

            foreach (var type in types)
            {
                BookType bookType = new BookType()
                {
                    Book = book,
                    Type = type
                };
                book.BookTypes.Add(bookType);
                type.BookTypes.Add(bookType);

                db.BookTypes.Add(bookType);
            }



        }
    }
}
