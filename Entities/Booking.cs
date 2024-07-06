namespace GraphQL_APIs.Entities;

public class Booking
{
    public  string Id { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTimeOffset Date { get; set; }
}