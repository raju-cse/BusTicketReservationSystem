
namespace Domain.Entities;

public class Passenger : BaseEntity
{
    public string Name { get; private set; }
    public string MobileNumber { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public List<Ticket> Tickets { get; private set; } = new();

    private Passenger() { }

    public Passenger(string name, string mobileNumber, string passwordHash, string email = null)
    {
        Name = name;
        MobileNumber = mobileNumber;
        PasswordHash = passwordHash;
        Email = email;
    }

    public void UpdateContact(string mobileNumber, string email)
    {
        MobileNumber = mobileNumber;
        Email = email;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetPassword(string passwordHash)
    {
        PasswordHash = passwordHash;
        UpdatedAt = DateTime.UtcNow;
    }
}
