using apbd_cw2_s33986.Domain;

namespace apbd_cw2_s33986.Services;

public class RentalService
{
    private readonly List<User> _users;
    private readonly List<Equipment> _equipment;
    private readonly List<Rental> _rentals;
    private readonly FeeCalculator _feeCalculator;

    public RentalService()
    {
        _users = new List<User>();
        _equipment = new List<Equipment>();
        _rentals = new List<Rental>();
        _feeCalculator =  new FeeCalculator();
    }

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public void AddEquipment(Equipment equipment)
    {
        _equipment.Add(equipment);
    }

    public List<Equipment> GetAvailableEquipment()
    {
        var availableEquipment = new List<Equipment>();
        foreach (var equipment in _equipment)
        {
            if (equipment.Status == EquipmentStatus.Available)
            {
                availableEquipment.Add(equipment);
            }
        }

        return availableEquipment;
    }

    public Rental RentEquipment(Guid userId, Guid equipmentId, int days)
    {
        User rentingUser = null;
        foreach (var user in _users)
        {
            if (user.Id == userId)
            {
                rentingUser = user;
                break;
            }
        }

        if (rentingUser == null)
        {
            throw new Exception("User not found");
        }
        
        Equipment rentedEquipment = null;
        foreach (var equipment in _equipment)
        {
            if (equipment.Id == equipmentId)
            {
                rentedEquipment = equipment;
                break;
            }
        }

        if (rentedEquipment == null)
        {
            throw new Exception("Equipment not found");
        }

        if (rentedEquipment.Status != EquipmentStatus.Available)
        {
            throw new  Exception("Selected equipment is not available");
        }

        int currentActiveRentals = 0;
        foreach (var rental in _rentals)
        {
            if (rental.User.Id == rentingUser.Id && rental.ReturnDate == null)
            {
                currentActiveRentals++;
            }
        }

        if (currentActiveRentals >= rentingUser.MaxRentals)
        {
            throw new Exception("User has reached maximum rentals");
        }

        var newRental = new Rental(rentingUser, rentedEquipment, days);
        rentedEquipment.Status = EquipmentStatus.Rented;
        _rentals.Add(newRental);
        return newRental;

    }

    public void ReturnEquipment(Guid rentalId, DateTime returnDate)
    {
        Rental returningRental = null;
        foreach (var rental in _rentals)
        {
            if (rental.Id == rentalId)
            {
                returningRental = rental;
                break;
            }
        }

        if (returningRental == null)
        {
            throw new Exception("Rental not found");
        }

        if (returningRental.ReturnDate != null)
        {
            throw new Exception("Equipment has already been returned");
        }
        
        decimal fee = _feeCalculator.CalculateFee(returningRental.RentalEndDate, returnDate);
        
        returningRental.EndRental(returnDate, fee);
        returningRental.Equipment.Status = EquipmentStatus.Available;
    }

    public List<Rental> GetOverdueRentals()
    {
        var overdueRentals = new List<Rental>();
        foreach (var rental in _rentals)
        {
            if (rental.IsOverdue)
            {
                overdueRentals.Add(rental);
            }
        }
        return overdueRentals;
    }

    public string GenerateSummaryReport()
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine($"Raport o stanie systemu na {DateTime.Now}\n");
        var available = GetAvailableEquipment();
        sb.AppendLine($"Aktualnie dostępny sprzęt: {available.Count} szt. :");
        if (available.Count == 0)
        {
            sb.AppendLine("Brak dostępnego sprzętu");
        }
        else
        {
            foreach (var equipment in available)
            {
                sb.AppendLine($"    -> {equipment.Name}, {equipment.Brand}, {equipment.Model}");
            }
        }

        sb.AppendLine();

        var overdue = GetOverdueRentals();
        sb.AppendLine($"Przeterminowane wypożyczenia:");
        if (overdue.Count == 0)
        {
            sb.AppendLine("Brak opoźnionych wypożyczeń");
        }
        else
        {
            foreach (var rental in overdue)
            {
                sb.AppendLine($"    -> Użytkownik {rental.User.FirstName} {rental.User.LastName}, Sprzęt: {rental.Equipment.Name}, {rental.Equipment.Brand}, {rental.Equipment.Model}.");
                sb.AppendLine($"    Termin minąl: {rental.RentalEndDate}");
            }
        }
        return sb.ToString();
    }
    
    
}