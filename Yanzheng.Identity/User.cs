using Microsoft.AspNetCore.Identity;
// Required for IdentityUser<TKey>

// Required for Guid and DateTime?

namespace Yanzheng.Identity;
// File-scoped namespace

public class User : IdentityUser<long>
{
    public Guid      PublicId                 { get; init; } // Initialize with a new Guid by default
    public required string    FirstName                { get; init; }
    public required string    LastName                 { get; init; } // Initialize to avoid null reference warnings
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public DateTime? RefreshTokenExpiresAtUtc { get; set; }

    /// <summary>
    ///     Used for optimistic concurrency control.
    ///     EF Core will automatically map this to a 'timestamp' or 'rowversion' column.
    /// </summary>
    public byte[]? RowVersion { get; set; }


    public static User Create(string email, string firstName, string lastName)
    {
        return new User
        {
            PublicId  = Guid.NewGuid(),
            Email     = email,
            FirstName = firstName,
            LastName  = lastName
        };
    }
}