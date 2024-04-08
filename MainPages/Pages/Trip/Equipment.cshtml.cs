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
    public class EquipmentModel : PageModel
    {
        private readonly Db db;

        public EquipmentModel()
        {
            db = new Db();
        }

        public TripsList TripsList { get; set; } = default!;

        public List<EquipmentList> EquipmentList { get;set; } = default!;

        public Dictionary<int, string> EquipmentDist { get; set; } = new Dictionary<int, string>();

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

            EquipmentList = await db.GetEquipmentByTripIdAsync((int)tripId);

            foreach (EquipmentList item in EquipmentList)
            {
                List<string> names = item.EquipmentDistributions.Select(d => d.ParticipantNumberNavigation).Select(p => p.UserNumberNavigation.Name).ToList();
                List<int> amounts = item.EquipmentDistributions.Select(d => d.Amount).ToList();

                string dist = "";

                if (names.Count > 0)
                {
                    dist = names[0] + " - " + amounts[0];

                    for (int i = 1; i < names.Count; i++)
                        dist += ", " + names[i] + " - " + amounts[i];
                }

                EquipmentDist[item.EquipmentNumber] = dist;
            }

            return Page();
        }
    }
}
