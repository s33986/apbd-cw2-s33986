namespace apbd_cw2_s33986.Domain;

public abstract class Equipment
{
    public Guid Id { get; private set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public EquipmentStatus Status { get; set; }

    protected Equipment(string name, string brand, string model)
    {
        Id = Guid.NewGuid();
        Name = name;
        Brand = brand;
        Model = model;
        Status = EquipmentStatus.Available;
    }
}