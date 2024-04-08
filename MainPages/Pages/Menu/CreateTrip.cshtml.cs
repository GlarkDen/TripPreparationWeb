using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MainPages.Models;
using System.ComponentModel;

namespace MainPages.Pages.Menu
{
    public class CreateTripModel : PageModel
    {
        private readonly Db db;

        public CreateTripModel()
        {
            db = new Db();
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            await GetTripTypesAsync();

            User? user = await db.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();
            else
                User = user;

            return Page();
        }

        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public TripsList TripsList { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            TripsList.TripTypeNumberNavigation = await db.GetTripTypeByIdAsync(TripsList.TripTypeNumber);
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

			await db.AddTripAsync(TripsList);

            ParticipantsList participant = new ParticipantsList();
			participant.UserNumber = User.UserNumber;
            participant.TripNumber = TripsList.TripNumber;

            Post? post = await db.GetPostByIdAsync(3);

            if (post == null)
                return RedirectToPage("Error");
            else
            {
                participant.PostNumbers.Add(post);
                await db.AddParticipantAsync(participant);

                TempData["Message"] = "Поход успешно создан";
                return RedirectToPage("/Menu/Trips", new { id = User.UserNumber });
            }
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
