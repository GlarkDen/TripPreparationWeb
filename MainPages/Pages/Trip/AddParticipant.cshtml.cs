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
    public class AddParticipantModel : PageModel
    {
        private readonly Db db;

        public AddParticipantModel()
        {
            db = new Db();
        }

        public User User { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchUser { get; set; }

        public List<User> FindUsers { get; set; } = new List<User>();

        public int TripId { get; set; }

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

            TripId = (int)tripId;

            if (SearchUser == null)
                SearchUser = "";

            TripsList? trip = await db.GetTripByIdAsync(TripId);
            List<int> tripUser;

			if (trip == null)
			{
				return NotFound();
			}
            else
            {
                tripUser = trip.ParticipantsLists.Select(p => p.UserNumberNavigation).Select(u => u.UserNumber).ToList();
			}

			List<User> users = await db.SearchUserByFullNameAsync(SearchUser);

            foreach (User us in users)
                if (!tripUser.Contains(us.UserNumber))
					FindUsers.Add(us);

			return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? userId, int? tripId, int? newUserId)
        {
            if (userId == null || tripId == null || newUserId == null)
            {
                return NotFound();
            }

			User? user = await db.GetUserByIdAsync((int)newUserId);

			if (user == null)
			{
				return NotFound();
			}
			else
			{
				Post? post = await db.GetPostByIdAsync(12);

				if (post == null)
					return RedirectToPage("/Error");
				else
				{
					ParticipantsList participant = new ParticipantsList();
					participant.UserNumber = user.UserNumber;
					participant.TripNumber = (int)tripId;

					participant.PostNumbers.Add(post);
					await db.AddParticipantAsync(participant);

					TempData["Message"] = "Участник успешно добавлен";
					return RedirectToPage("/Trip/Participants", new { userId = userId, tripId = tripId });
				}
			}
		}
    }
}
