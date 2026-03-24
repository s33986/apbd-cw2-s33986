namespace apbd_cw2_s33986.Domain;

public class Laptop : Equipment
{
    public int Ram { get; set; }
    public string Processor { get; set; }

    public Laptop(string name, string brand, string model, int ram, string processor) : base(name, brand, model)
    {
        Ram = ram;
        Processor = processor;
    }
}