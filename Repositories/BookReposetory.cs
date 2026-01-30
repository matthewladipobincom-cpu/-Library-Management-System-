using Microsoft.EntityFrameworkCore;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
        => await _context.Books.ToListAsync();

    public async Task<Book?> GetByIdAsync(int id)
        => await _context.Books.FindAsync(id);

    public async Task AddAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Book book)
    {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<object>> GetBooksGroupedByAuthorAsync()
{
    return await _context.Books
        .GroupBy(b => b.Author)
        .Select(g => new
        {
            Author = g.Key,
            Books = g.Select(b => new
            {
                b.Id,
                b.Title,
                b.ISBN,
                b.YearPublished
            })
        })
        .ToListAsync();
}

public async Task<IEnumerable<Book>> GetTopBorrowedBooksAsync(int count)
{
    return await _context.Books
        .OrderByDescending(b => b.TimesBorrowed)
        .Take(count)
        .ToListAsync();
}

}
