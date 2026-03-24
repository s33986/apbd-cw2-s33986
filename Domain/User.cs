namespace apbd_cw2_s33986.Domain;

public abstract class User
{
    public Guid Id { get; private set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserType Type { get; private set; }
    
    public abstract int MaxRentals { get; }

    protected User(string firstName, string lastName, UserType type)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Type = type;
    }
}