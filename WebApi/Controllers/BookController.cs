using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.GetBooks;
using WebApi.DBOperations;
using WebApi.BookOperations.CreateBook;
using System;
using WebApi.BookOperations.UpdateBook;
using WebApi.BookOperations.DeleteBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;
using static WebApi.BookOperations.GetBooks.GetBookDetailQuery;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }
        

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery bookquery = new GetBooksQuery(_context);
            var result = bookquery.Handle();
            return Ok(result);

        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context);
                query.BookId = id;
                result = query.Handle();
            }
            catch (Exception ex)
            {
              return BadRequest(ex.Message);
            }

            return Ok(result);
         }

        //[HttpGet]
        //public Book Get([FromQuery] string id)
        //{

        //    var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //    return book;
        //} 

        //Post
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookViewModel newBook)
        {

            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
              return Ok();

        } 
        //Put
        [HttpPut("id")]
       public IActionResult UpdateBook(int id,[FromBody] UpdateBookViewModel updatedBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updatedBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
               
            }
           
           
            return Ok();

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok();
        }

    }

}