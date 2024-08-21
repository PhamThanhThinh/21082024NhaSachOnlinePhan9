using Microsoft.EntityFrameworkCore;
using NhaSachOnline.Data;
using NhaSachOnline.Models;

namespace NhaSachOnline.Repositories
{
  public class HomeRepository : IHomeRepository
  {
    private readonly ApplicationDbContext _dbContext;

    public HomeRepository(ApplicationDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    // Task là một class trong C#.NET
    // Task dùng cho xử lý bất đồng bộ
    // sử dụng cùng với các từ khóa: async, await, cùng với các phương thức
    // có sử dụng từ khóa async như: ToListAsync()
    // Task đại diện cho một công việc đang thực hiện, ở đây là công việc lấy
    // thông tin sách (GetBooks)
    public async Task<IEnumerable<Book>> GetBooks(string keySearch = "", int genreId = 0)
    {
      // chuyển đổi chuỗi sang dạng chữ thường
      keySearch = keySearch.ToLower();

      IEnumerable<Book> books =
        await (from book in _dbContext.Books
               join genre in _dbContext.Genres
               on book.GenreId equals genre.Id
               where string.IsNullOrWhiteSpace(keySearch) ||
               (book != null && book.BookName != null &&
               book.BookName.ToLower().StartsWith(keySearch.ToLower()))
               select new Book
               {
                 Id = book.Id,
                 Image = book.Image,
                 AuthorName = book.AuthorName,
                 BookName = book.BookName,
                 GenreId = book.GenreId,
                 Price = book.Price,
                 GenreName = genre.GenreName
               }
               ).ToListAsync();

      if (genreId > 0)
      {
        books = books.Where(i => i.GenreId == genreId).ToList();
      }

      return books;
    }

    //public async Task<IEnumerable<Book>> LayThongTinSachTuDatabase(string keySearch = "", int theLoaiId = 0)
    //{
    //  // chuyển đổi chuỗi sang dạng chữ thường
    //  keySearch = keySearch.ToLower();

    //  IEnumerable<Book> books = 
    //    await (from sach in _dbContext.Books
    //          join theLoai in _dbContext.Genres
    //          on sach.GenreId equals theLoai.Id
    //          where string.IsNullOrWhiteSpace(keySearch) ||
    //          (sach != null && sach.BookName != null &
    //          sach.BookName.ToLower().StartsWith(keySearch.ToLower()))
    //          select new Book
    //          {

    //          }
    //          ).ToListAsync();
    //  return books;
    //}
  }
}
