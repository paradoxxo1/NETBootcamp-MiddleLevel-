namespace DomainDrivenDesign.Domain.Products;

public sealed record Name
{
    public string Value { get; init; }
    public Name(string value)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Name cannot be empty");
        }
        if (value.Length < 3)
        {
            throw new ArgumentException("Name must be at least 3 characters");
        }

        Value = value;

    }
}
