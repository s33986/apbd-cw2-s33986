namespace apbd_cw2_s33986.Domain;

public class Camera : Equipment
{
    public double Megapixels { get; set; }
    public string Type { get; set; }

    public Camera(string name, string brand, string model, double megapixels, string type) : base(name, brand, model)
    {
        Megapixels = megapixels;
        Type = type;
    }
}