using Microsoft.AspNetCore.Mvc;
using YourProjectNamespace.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace YourProjectNamespace.Controllers
{
    public class OrderController : Controller
    {
        // Simulated DB
        private static List<Order> Orders = new List<Order>
        {
            new Order { OrderId = 1, UserId = 101, TotalAmount = 199.99M, OrderDate = DateTime.Now.AddDays(-3), Status = "PENDING" },
            new Order { OrderId = 2, UserId = 102, TotalAmount = 150.00M, OrderDate = DateTime.Now.AddDays(-1), Status = "SHIPPED" }
        };

        public IActionResult Index()
        {
            // Simulate role ("admin" or "customer")
            ViewBag.Role = "admin"; // Change to "customer" to test
            return View(Orders);
        }

        public IActionResult Details(int orderId)
        {
            var order = Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
                return NotFound();

            ViewBag.Role = "admin"; // Or "customer"
            return View(order);
        }

        [HttpPost]
        public IActionResult Create(int userId, decimal totalAmount)
        {
            var newOrder = new Order
            {
                OrderId = Orders.Max(o => o.OrderId) + 1,
                UserId = userId,
                TotalAmount = totalAmount,
                OrderDate = DateTime.Now,
                Status = "PENDING"
            };
            Orders.Add(newOrder);
            return Json(newOrder);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int orderId, string status)
        {
            var order = Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                order.Status = status;
            }
            return Json(order);
        }

        [HttpPost]
        public IActionResult Delete(int orderId)
        {
            Orders = Orders.Where(o => o.OrderId != orderId).ToList();
            return Json(new { success = true });
        }
    }
}
