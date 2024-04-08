using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MainPages.Models;

namespace MainPages.Pages.Menu
{
	public class LeaveTripModel : PageModel
	{
		private readonly Db db;

		public LeaveTripModel()
		{
			db = new Db();
		}

		public TripsList Trip { get; set; } = default!;

        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? userid, int? tripid)
		{
			if (userid == null || tripid == null)
			{
				return NotFound();
			}

            User? user = await db.GetUserByIdAsync((int)userid);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                User = user;
            }

            var trip = await db.GetTripByIdAsync((int)tripid);

			if (trip == null)
			{
				return NotFound();
			}
			else
				Trip = trip;

			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? userid, int? tripid)
		{
			if (userid == null || tripid == null)
			{
				return NotFound();
			}

			ParticipantsList? participant = await db.GetParticipantByUserAndTripAsync((int)userid, (int)tripid);

			if (participant == null)
				RedirectToPage("/Error");
			else
				await db.DeleteParticipantAsync(participant);

			int participantCount = await db.GetTripParticipantCountAsync((int)tripid);

			if (participantCount == 0)
			{
				TripsList? trip = await db.GetTripByIdAsync((int)tripid);

				if (trip == null)
					RedirectToPage("/Error");
				else
					await db.DeleteTripAsync(trip);
			}

            return RedirectToPage("/Menu/Trips", new { id = userid });
		}
	}
}
