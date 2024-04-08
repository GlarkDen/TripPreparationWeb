using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MainPages.Models;

namespace MainPages.Pages.Trip
{
    public class EditModel : PageModel
    {
        private readonly Db db;

        public EditModel()
        {
            db = new Db();
        }

        [BindProperty]
        public TripsList TripsList { get; set; } = default!;

        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? tripId, int? userId)
        {
            await GetTripTypesAsync();

            if (tripId == null || userId == null)
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

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.ClearValidationState("TripsList.TripTypeNumberNavigation");
            ModelState.MarkFieldValid("TripsList.TripTypeNumberNavigation");

            if (!ModelState.IsValid)
            {
                await GetTripTypesAsync();
                return Page();
            }

            if (DataChecker.CheckDateNow(TripsList.StartDate) == -1)
            {
                ModelState.AddModelError("TripsList.StartDate", "Указанная дата уже прошла");
                await GetTripTypesAsync();
                return Page();
            }

            if (TripsList.StartDate > TripsList.EndDate)
            {
                ModelState.AddModelError("TripsList.EndDate", "Дата окончания не может быть меньше даты начала");
                await GetTripTypesAsync();
                return Page();
            }

            await db.UpdateTripAsync(TripsList);

            TempData["Message"] = "Изменения успешно сохранены";
            return RedirectToPage("/Trip/Description", new { userId = User.UserNumber, tripId = TripsList.TripNumber });
        }

        public async Task GetTripTypesAsync()
        {
            List<TripType> tripTypes = await db.GetTripTypesAsync();

            var items = tripTypes.Select(trip => new SelectListItem
            {
                Text = trip.Type.ToString(),
                Value = trip.TripTypeNumber.ToString()
            });

            ViewData["TripTypeNumber"] = items;
        }
    }
}
