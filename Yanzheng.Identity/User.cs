using Microsoft.AspNetCore.Identity; // Required for IdentityUser<TKey>
using System;                        // Required for Guid and DateTime?

namespace Yanzheng.Identity;
// File-scoped namespace

public class User : IdentityUser<long>
{
    public Guid      PublicId  { get; set; } = Guid.NewGuid(); // Initialize with a new Guid by default
    public string    FirstName { get; set; } = "";
    public string    LastName  { get; set; } = ""; // Initialize to avoid null reference warnings
    public DateTime? RefreshTokenExpiresAtUtc { get; set; }

    /// <summary>
    /// Used for optimistic concurrency control.
    /// EF Core will automatically map this to a 'timestamp' or 'rowversion' column.
    /// </summary>
    public byte[]? RowVersion { get; set; }


    public static User Create(string email, string firstName, string lastName)
    {
        return new User
        {
            PublicId  = Guid.NewGuid(),
            Email     = email,
            FirstName = firstName,
            LastName  = lastName,
        };
    }
}