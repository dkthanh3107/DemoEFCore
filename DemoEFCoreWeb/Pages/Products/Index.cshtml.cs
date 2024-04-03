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
    public class IndexModel : PageModel
    {
        private readonly ConsotoPizza.Data.ContosoPizzaContext _context;

        public IndexModel(ConsotoPizza.Data.ContosoPizzaContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Products != null)
            {
                Product = await _context.Products.ToListAsync();
            }
        }
    }
}
