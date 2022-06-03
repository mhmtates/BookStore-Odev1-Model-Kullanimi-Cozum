using WebApi.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public UpdateBookViewModel Model { get; set; }
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book == null)
                throw new InvalidOperationException("Güncellenecek Kitap Bulunamadı!");
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;//Kitabın türü değiştirilmişse yeni kitabın türünü kullan,değiştirilmemişse mevcut kitabın türünü kullan.
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _dbContext.SaveChanges();
        }

        public class UpdateBookViewModel
        {
            public string Title { get; set; }

            public int GenreId { get; set; }
        }
    }
}
