namespace apbd_cw2_s33986.Domain;

public class Student : User
{
    public string IndexNumber { get; set; }

    public override int MaxRentals => 2;

    public Student(string firstName, string lastName, UserType type, string indexNumber) : base(firstName, lastName, UserType.Student)
    {
        IndexNumber = indexNumber;
    }
}