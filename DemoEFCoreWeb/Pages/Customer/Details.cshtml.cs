using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConsotoPizza.Data;
using ContosoPizza.Models;

namespace DemoEFCoreWeb.Pages.Customer
{
    public class DetailsModel : PageModel
    {
        private readonly ConsotoPizza.Data.ContosoPizzaContext _context;

        public DetailsModel(ConsotoPizza.Data.ContosoPizzaContext context)
        {
            _context = context;
        }

        public int CustomerID { get; private set; }
        public ContosoPizza.Models.Customer? Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int customerID)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            Customer = await _context.Customers.FindAsync(id);
            CustomerID = customerID;

            if (Customer == null)
            {
                return NotFound();
            }

            if (customerID > 0)
            {
                // Sử dụng FromSqlInterpolated để tạo truy vấn tương tác với cơ sở dữ liệu
                Customer = await _context.Customers
                    .FromSqlInterpolated($"SELECT * FROM Customers WHERE CustomerId = {id}")
                    .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderDetails)
                    .ThenInclude(od => od.Products)
                    .FirstOrDefaultAsync();
            }

            return Page();
        }
    }
}