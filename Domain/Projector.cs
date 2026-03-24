namespace apbd_cw2_s33986.Domain;

public class Projector : Equipment
{
    public string Resolution { get; set; }
    public int Luminance { get; set; }

    public Projector(string name, string brand, string model, string resolution, int luminance) : base(name, brand, model)
    {
        Resolution = resolution;
        Luminance = luminance;
    }
}