using Bogus;
using Bookify.Application.Abstractions.Authentication;
using Bookify.Application.Abstractions.Data;
using Bookify.Domain.Abstraction;
using Bookify.Domain.Apartments;
using Bookify.Domain.Users;
using Dapper;

namespace Bookify.Api.Extensions;

public static class SeedDataExtensions
{
    private const string AdminEmail = "admin@bookify.com";

    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();

        using var connection = sqlConnectionFactory.CreateConnection();

        var faker = new Faker();

        List<object> apartments = [];

        for (int i = 0; i < 100; ++i)
        {
            apartments.Add(new
            {
                Id = Guid.NewGuid(),
                Name = faker.Company.CompanyName(),
                Description = "Amazing view",
                Country = faker.Address.Country(),
                State = faker.Address.State(),
                ZipCode = faker.Address.ZipCode(),
                City = faker.Address.City(),
                PriceAmount = faker.Random.Decimal(50, 1000),
                PriceCurrency = "USD",
                CleaningFeeAmount = faker.Random.Decimal(25, 200),
                CleaningFeeCurrency = "USD",
                Amenities = new List<int> { (int)Amenity.Parking, (int)Amenity.MountainView },
                LastBookedUtc = DateTime.MinValue
            });
        }

        const string sql = """
            INSERT INTO public.apartments
            (id, name, description, address_country, address_state, address_zip_code, address_city, price_amount, price_currency, cleaning_fee_amount, cleaning_fee_currency, amenities, last_booked_utc)
            VALUES(@Id, @Name, @Description, @Country, @State, @ZipCode, @City, @PriceAmount, @PriceCurrency, @CleaningFeeAmount, @CleaningFeeCurrency, @Amenities, @LastBookedUtc);
            """;

        connection.Execute(sql, apartments);
    }

    public static async Task SeedAdmin(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();

        if (await userRepo.GetByEmailAsync(AdminEmail) is not null)
            return;

        var authenticationService = scope.ServiceProvider.GetRequiredService<IAuthenticationService>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var user = User.Create(
            new FirstName("admin"),
            new LastName("admin"),
            new Email("admin@bookify.com"));

        var identityId = await authenticationService.RegisterAsync(user, "admin");
        user.SetIdentityId(identityId);
        user.AssignRole(Role.Admin);

        userRepo.Add(user);
        await unitOfWork.SaveChangesAsync();
    }
}
