public class Order
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }

    // Add this if you want to directly show customer name
    public string CustomerName { get; set; }
}
