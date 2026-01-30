public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public required string ISBN { get; set; }
    public int YearPublished { get; set; }

    // Mock borrowing count
    public int TimesBorrowed { get; set; }
}
