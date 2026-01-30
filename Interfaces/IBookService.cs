public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book?> GetBookByIdAsync(int id);
    Task<Book> CreateBookAsync(Book book);
    Task<bool> UpdateBookAsync(int id, Book book);
    Task<bool> DeleteBookAsync(int id);

    Task<IEnumerable<object>> GetBooksGroupedByAuthorAsync();
    Task<IEnumerable<Book>> GetTopBorrowedBooksAsync();

}
