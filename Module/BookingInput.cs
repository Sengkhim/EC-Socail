namespace GraphQL_APIs.Module;

public record BookingInput
{
    public string Text { get; set; } = string.Empty;
    
    public string? Description { get; set; }
}