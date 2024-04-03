using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConsotoPizza.Data;
using ContosoPizza.Models;

namespace DemoEFCoreWeb.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly ConsotoPizza.Data.ContosoPizzaContext _context;
        private int CustomerId;

        public DetailsModel(ConsotoPizza.Data.ContosoPizzaContext context)
        {
            _context = context;
        }

        public Product? Product { get; set; } = default!;
        public ContosoPizza.Models.Customer? Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int customerID)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            Product = await _context.Products.FindAsync(id);
            if (Product == null)
            {
                return NotFound();
            }
            CustomerId = customerID;
            if (CustomerId > 0)
            {
                Customer = await _context.Customers
                    .FromSqlInterpolated($"SELECT * FROM Customers Where CustomerId = {CustomerId}")
                    .FirstOrDefaultAsync();
            }
            return Page();
        }
    }
}