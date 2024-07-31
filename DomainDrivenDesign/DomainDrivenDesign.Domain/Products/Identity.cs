namespace DomainDrivenDesign.Domain.Products;

public sealed record Identity
{
    public Guid Value { get; init; }
    public Identity(Guid value)
    {
        //kontroller
        Value = value;
    }

}
