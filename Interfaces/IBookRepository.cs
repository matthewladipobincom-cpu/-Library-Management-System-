public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(int id);
    Task AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(Book book);

     Task<IEnumerable<object>> GetBooksGroupedByAuthorAsync();
    Task<IEnumerable<Book>> GetTopBorrowedBooksAsync(int count);
}
