using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MainPages.Models;

namespace MainPages.Pages.Trip
{
    public class ProductsModel : PageModel
    {
        private readonly Db db;

        public ProductsModel()
        {
            db = new Db();
        }

        public List<ProductsList> ProductsList { get; set; } = default!;

        public TripsList TripsList { get; set; } = default!;

        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? userId, int? tripId)
        {
            if (userId == null || tripId == null)
            {
                return NotFound();
            }

            User? user = await db.GetUserByIdAsync((int)userId);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                User = user;
            }

            var tripslist = await db.GetTripByIdAsync((int)tripId);
            if (tripslist == null)
            {
                return NotFound();
            }
            else
            {
                TripsList = tripslist;
            }

            ProductsList = await db.GetProductByTripIdAsync((int)tripId);

            return Page();
        }
    }
}
