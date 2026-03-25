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
    
    
}