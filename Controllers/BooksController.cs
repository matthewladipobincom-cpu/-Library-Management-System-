using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

// [ApiController]
// [Route("api/books")]
// public class BooksController : ControllerBase
// {
//     private readonly AppDbContext _context;

//     public BooksController(AppDbContext context)
//     {
//         _context = context;
//     }

//     // User & Admin
//     [Authorize(Roles = "User,Admin")]
//     [HttpGet]
//     public async Task<IActionResult> GetAllBooks()
//     {
//         return Ok(await _context.Books.ToListAsync());
//     }

//     // User & Admin
//     [Authorize(Roles = "User,Admin")]
//     [HttpGet("{id}")]
//     public async Task<IActionResult> GetBook(int id)
//     {
//         var book = await _context.Books.FindAsync(id);
//         if (book == null) return NotFound();
//         return Ok(book);
//     }

//     // Admin only
//     [Authorize(Roles = "Admin")]
//     [HttpPost]
//     public async Task<IActionResult> AddBook(Book book)
//     {
//         _context.Books.Add(book);
//         await _context.SaveChangesAsync();
//         return Ok(book);
//     }

//     // Admin only
//     [Authorize(Roles = "Admin")]
//     [HttpPut("{id}")]
//     public async Task<IActionResult> UpdateBook(int id, Book book)
//     {
//         if (id != book.Id) return BadRequest();

//         _context.Entry(book).State = EntityState.Modified;
//         await _context.SaveChangesAsync();
//         return NoContent();
//     }

//     //  Admin only
//     [Authorize(Roles = "Admin")]
//     [HttpDelete("{id}")]
//     public async Task<IActionResult> DeleteBook(int id)
//     {
//         var book = await _context.Books.FindAsync(id);
//         if (book == null) return NotFound();

//         _context.Books.Remove(book);
//         await _context.SaveChangesAsync();
//         return NoContent();
//     }
// }


[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _service;

    public BooksController(IBookService service)
    {
        _service = service;
    }

    [Authorize(Roles = "User,Admin")]
    [HttpGet]
    public async Task<IActionResult> GetBooks()
        => Ok(await _service.GetAllBooksAsync());

    [Authorize(Roles = "User,Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        var book = await _service.GetBookByIdAsync(id);
        return book == null ? NotFound() : Ok(book);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(Book book)
        => Ok(await _service.CreateBookAsync(book));

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Book book)
        => await _service.UpdateBookAsync(id, book) ? NoContent() : NotFound();

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
        => await _service.DeleteBookAsync(id) ? NoContent() : NotFound();


        /// <summary>
/// Retrieve books grouped by author
/// </summary>
[Authorize(Roles = "User,Admin")]
[HttpGet("grouped-by-author")]
public async Task<IActionResult> GetBooksGroupedByAuthor()
{
    return Ok(await _service.GetBooksGroupedByAuthorAsync());
}

/// <summary>
/// Retrieve top 3 most borrowed books
/// </summary>
[Authorize(Roles = "User,Admin")]
[HttpGet("top-borrowed")]
public async Task<IActionResult> GetTopBorrowedBooks()
{
    return Ok(await _service.GetTopBorrowedBooksAsync());
}



/// <summary>
/// Simulates fetching book details from an external API
/// </summary>
[Authorize(Roles = "User,Admin")]
[HttpGet("{id}/external-details")]
public async Task<IActionResult> GetExternalBookDetails(
    int id,
    [FromServices] IExternalBookService externalService)
{
    var data = await externalService.FetchBookDetailsAsync(id);
    return Ok(data);
}

}
