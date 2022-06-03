using WebApi.Common;
using WebApi.DBOperations;
using System;
using System.Linq;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBookDetailQuery
    {
       
        private readonly BookStoreDbContext _dbContext;

        public int BookId { get; set; }
        public GetBookDetailQuery(BookStoreDbContext  dbContext)
        {
            _dbContext = dbContext;
        }
        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();
            if (book == null)
                throw new InvalidOperationException("Kitap Bulunamadı.");
            BookDetailViewModel bvm = new BookDetailViewModel();
            bvm.Title = book.Title;
            bvm.Genre = ((Genre)book.GenreId).ToString();
            bvm.PageCount = book.PageCount;
            bvm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            return bvm;
        }
      
        public class BookDetailViewModel
        {
            public string Title { get; set; }

            public string Genre { get; set; }

            public int PageCount { get; set; }

            public string PublishDate { get; set; }
        }
            
        }

    }

