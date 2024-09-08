using Bookify.Domain.Abstraction;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Apartments;
public sealed class Apartment : Entity
{
    public Apartment(
        Guid id,
        Name name,
        Description description,
        Address address,
        Money price,
        Money cleaningFee,
        DateTime lastBookedUtc,
        List<Amenity> amenities)
        : base(id)
    {
        Name = name;
        Description = description;
        Address = address;
        Price = price;
        CleaningFee = cleaningFee;
        LastBookedUtc = lastBookedUtc;
        Amenities = amenities;
    }

    public Name Name { get; private set; }

    public Description Description { get; private set; }

    public Address Address { get; private set; }

    public Money Price { get; private set; }

    public Money CleaningFee { get; private set; }

    public DateTime? LastBookedUtc { get; internal set; }

    public List<Amenity> Amenities { get; private set; } = [];

    private Apartment()
    { }
}