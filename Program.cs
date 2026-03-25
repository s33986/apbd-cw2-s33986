using apbd_cw2_s33986.Domain;
using apbd_cw2_s33986.Services;

namespace apbd_cw2_s33986;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting simulation");

        var service = new RentalService();
        
        var laptop1 = new Laptop("Laptop MacBook 1", "Apple", "Macbook Air 13'", 16, "M3");
        var laptop2 = new Laptop("Laptop Windows 1", "Lenovo", "Thinkpad", 16, "i5");

        var projector = new Projector("Projektor przenośny 1", "BENQ", "GV50", "Full HD", 500);

        var camera = new Camera("Aparat fotograficzny 1", "Panasonic", "Lumix S9", 25, "full-frame");
        
        service.AddEquipment(laptop1);
        service.AddEquipment(laptop2);
        service.AddEquipment(projector);
        service.AddEquipment(camera);

        var student1 = new Student("Jan", "Bobrowski", "s2137");
        var student2 = new Student("Krzysztof", "Krawczyk", "s6767");

        var lecturer = new Employee("Tomasz", "Michałowski", "wykładowca");
        var bssworker = new Employee("Adam", "Nowak", "pracownik bss");
        
        service.AddUser(student1);
        service.AddUser(student2);
        service.AddUser(bssworker);
        service.AddUser(lecturer);
        
        Console.WriteLine("\nUse case 1: correct rental");
        var correctRental = service.RentEquipment(student1.Id, laptop1.Id, 5);
        Console.WriteLine($"Student {student1.FirstName} {student1.LastName} has rented {laptop1.Name} for 5 days");
        Console.WriteLine($"{laptop1.Name} current status: {laptop1.Status}");
        
        Console.WriteLine("\nUse case 2: attempt to rent unavailable equipment");
        try
        {
            service.RentEquipment(lecturer.Id, laptop1.Id, 3);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        
        Console.WriteLine("\nUse case 3: attempt to exceed rental limit");
        service.RentEquipment(student1.Id, projector.Id, 5);
        try
        {
            service.RentEquipment(student1.Id, camera.Id, 3);
        } catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        Console.WriteLine($"{camera.Name} current status: {camera.Status}");
        
        Console.WriteLine("\nUse case 4: return on time");
        
        service.ReturnEquipment(correctRental.Id, DateTime.Now.AddDays(3));
        Console.WriteLine($"Correctly returned {laptop1.Name}, additional fee: {correctRental.Fee}");
        Console.WriteLine($"{laptop1.Name} current status: {laptop1.Status}");

        Console.WriteLine("\nUse case 5: late return with additional fee");
        var lateRental = service.RentEquipment(student2.Id, laptop2.Id, 2);
        Console.WriteLine($"Student {student2.FirstName} {student2.LastName} has rented {laptop2.Name} for 2 days");
        Console.WriteLine($"{laptop2.Name} current status: {laptop2.Status}");
        
        service.ReturnEquipment(lateRental.Id, DateTime.Now.AddDays(4));
        Console.WriteLine($"Correctly returned {laptop2.Name}, additional fee: {lateRental.Fee}");
        Console.WriteLine($"{laptop2.Name} current status: {laptop2.Status}");

        string finalReport = service.GenerateSummaryReport();
        Console.WriteLine($"\n {finalReport}");
    }
}