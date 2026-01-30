public class BookService : IBookService
{
    private readonly IBookRepository _repo;

    public BookService(IBookRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
        => await _repo.GetAllAsync();

    public async Task<Book?> GetBookByIdAsync(int id)
        => await _repo.GetByIdAsync(id);

    public async Task<Book> CreateBookAsync(Book book)
    {
        await _repo.AddAsync(book);
        return book;
    }

    public async Task<bool> UpdateBookAsync(int id, Book book)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) return false;

        existing.Title = book.Title;
        existing.Author = book.Author;
        existing.ISBN = book.ISBN;
        existing.YearPublished = book.YearPublished;

        await _repo.UpdateAsync(existing);
        return true;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _repo.GetByIdAsync(id);
        if (book == null) return false;

        await _repo.DeleteAsync(book);
        return true;
    }

    public async Task<IEnumerable<object>> GetBooksGroupedByAuthorAsync()
{
    return await _repo.GetBooksGroupedByAuthorAsync();
}

public async Task<IEnumerable<Book>> GetTopBorrowedBooksAsync()
{
    return await _repo.GetTopBorrowedBooksAsync(3);
}

}
