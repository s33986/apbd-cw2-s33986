namespace apbd_cw2_s33986.Domain;

public class Rental
{
    public Guid Id { get; set; }
    
    public User User { get; private set; }
    public Equipment Equipment { get; private set; }
    
    public DateTime RentalDate { get; private set; }
    public DateTime RentalEndDate { get; private set; }
    
    public DateTime? ReturnDate { get; private set; }
    
    public decimal Fee { get; private set; }

    public Rental(User user, Equipment equipment, int rentalTime)
    {
        Id = Guid.NewGuid();
        User = user;
        Equipment = equipment;
        RentalDate = DateTime.Now;
        RentalEndDate = RentalDate.AddDays(rentalTime);
        ReturnDate = null;
        
        Fee = 0;
    }

    public void EndRental(DateTime rentalEndDate, decimal fee)
    {
        ReturnDate = rentalEndDate;
        Fee = fee;
    }
    
    public bool IsOverdue => !ReturnDate.HasValue && DateTime.Now > RentalEndDate;
}