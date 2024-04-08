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
	public class DeleteParticipantModel : PageModel
	{
		private readonly Db db;

		public DeleteParticipantModel()
		{
			db = new Db();
		}

		[BindProperty]
		public ParticipantsList ParticipantsList { get; set; } = default!;

        public User User { get; set; }

        public int TripId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? tripId, int? userId, int? partId)
		{
			if (tripId == null || userId == null || partId == null)
			{
				return NotFound();
			}

            TripId = (int)tripId;

            User? user = await db.GetUserByIdAsync((int)userId);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                User = user;
            }

            var part = await db.GetParticipantsByIdAsync((int)partId);
            if (part == null)
            {
                return NotFound();
            }
            else
            {
                ParticipantsList = part;
            }

            return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? tripId, int? userId, int? partId)
		{
            if (tripId == null || userId == null || partId == null)
            {
                return NotFound();
            }

            await db.DeleteParticipantAsync(ParticipantsList);

			return RedirectToPage("/Trip/Participants", new { tripId = tripId, userId = userId });
		}
	}
}
