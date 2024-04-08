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
    public class FoodModel : PageModel
    {
        private readonly Db db;

        public FoodModel()
        {
            db = new Db();
        }

        public TripsList TripsList { get; set; } = default!;

        public User User { get; set; }

        public List<Food> Foods { get; set; }

        public Dictionary<int, List<string>> Names { get; set; } = new Dictionary<int, List<string>>();

        public Dictionary<int, List<int>> Amounts { get; set; } = new Dictionary<int, List<int>>();

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

            Foods = await db.GetFoodByTripIdAsync((int)tripId);

            foreach (Food item in Foods)
            {
                Names[item.FoodNumber] = item.ProductsForFoods.Select(p => p.ProductNumberNavigation.ProductName).ToList();
                Amounts[item.FoodNumber] = item.ProductsForFoods.Select(p => p.Amount).ToList();
            }

            return Page();
        }
    }
}
