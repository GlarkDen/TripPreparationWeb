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
    public class SetLeaderModel : PageModel
    {
        private readonly Db db;

        public SetLeaderModel()
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

            Post post = await db.GetPostByIdAsync(3);

            ParticipantsList.PostNumbers.Add(post);
            ParticipantsList oldLeader = User.ParticipantsLists.First(p => p.TripNumber == tripId);

            if (oldLeader.PostNumbers.Count == 1)
                oldLeader.PostNumbers.Add(await db.GetPostByIdAsync(12));

            oldLeader.PostNumbers.Remove(post);

            await db.UpdateParticipantAsync(oldLeader);
            await db.UpdateParticipantAsync(ParticipantsList);

            TempData["Message"] = $"{ParticipantsList.UserNumberNavigation.Name} {ParticipantsList.UserNumberNavigation.Patronymic} назначен руководителем";

            return RedirectToPage("/Trip/Participants", new { tripId = tripId, userId = userId });
        }
    }
}
