// OrderController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourProjectNamespace.Models; // Replace with your actual namespace
using System.Linq;
using System.Threading.Tasks;

public class OrderController : Controller
{
    private readonly YourDbContext _context; // Replace with your actual DbContext

    public OrderController(YourDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var orders = _context.Orders
            .Include(o => o.User) // Assuming Order has a navigation property to User
            .Select(o => new OrderViewModel
            {
                OrderId = o.OrderId,
                TotalAmount = o.TotalAmount,
                OrderDate = o.OrderDate,
                Status = o.Status,
                CustomerName = o.User.Name // Replace with actual User property
            }).ToList();

        ViewBag.Role = User.IsInRole("admin") ? "admin" : "customer";

        return View(orders);
    }
}
