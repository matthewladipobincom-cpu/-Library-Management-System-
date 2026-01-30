public interface IExternalBookService
{
    Task<object> FetchBookDetailsAsync(int bookId);
}
