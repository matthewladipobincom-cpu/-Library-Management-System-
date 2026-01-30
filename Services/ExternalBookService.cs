public class ExternalBookService : IExternalBookService
{
    public async Task<object> FetchBookDetailsAsync(int bookId)
    {
        // Simulate network delay
        await Task.Delay(2000);

        return new
        {
            BookId = bookId,
            ReviewSource = "External API",
            Rating = 4.5,
            Reviews = 120
        };
    }
}
