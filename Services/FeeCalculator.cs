namespace apbd_cw2_s33986.Services;

public class FeeCalculator
{
    private readonly decimal _dailyFee = 10;

    public decimal CalculateFee(DateTime dueDate, DateTime returnDate)
    {
        if (returnDate <= dueDate)
        {
            return 0;
        }

        TimeSpan delay = returnDate - dueDate;

        int daysLate = (int)Math.Ceiling(delay.TotalDays);
        
        return daysLate * _dailyFee;
    }
}