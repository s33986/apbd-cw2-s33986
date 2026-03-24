namespace apbd_cw2_s33986.Domain;

public class Employee : User
{
    public string Position { get; set; }

    public override int MaxRentals => 5;

    public Employee(string firstName, string lastName, UserType type, string position) : base(firstName, lastName, type)
    {
        Position = position;
    }
}