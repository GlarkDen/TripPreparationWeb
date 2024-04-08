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
    public class EditPostsModel : PageModel
    {
        private readonly Db db;

        public EditPostsModel()
        {
            db = new Db();
        }

		[BindProperty]
		public User User { get; set; }

		[BindProperty]
		public ParticipantsList Participant { get; set; } = default!;

		[BindProperty]
        public Post AddPost { get; set; } = new Post();

		[BindProperty]
		public int TripId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? userId, int? tripId, int? partId)
        {
            if (userId == null || tripId == null || partId == null)
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
				Participant = part;
			}

			TripId = (int)tripId;

			await GetPostsAsync((int)partId);
			return Page();
        }

		public async Task<IActionResult> OnPostDeleteAsync(int? userId, int? tripId, int? partId, int? postId)
		{
			if (userId == null || tripId == null || postId == null || partId == null)
			{
				return NotFound();
			}

			Post? post = await db.GetPostByIdAsync((int)postId);

			if (post == null)
				return RedirectToPage("/Error");
			else
			{
				ParticipantsList? participant = await db.GetParticipantsByIdAsync((int)partId);

				if (participant == null)
					return NotFound();

				if (participant.PostNumbers.Count == 1)
					participant.PostNumbers.Add(await db.GetPostByIdAsync(12));

				participant.PostNumbers.Remove(post);

				await db.UpdateParticipantAsync(participant);
			}

			await GetPostsAsync((int)partId);
			return RedirectToPage();
		}

		public async Task<IActionResult> OnPostAddAsync(int? userId, int? tripId, int? partId)
		{
			if (userId == null || tripId == null || partId == null)
			{
				return NotFound();
			}

			Post? post = await db.GetPostByIdAsync(AddPost.PostNumber);

			if (post == null)
				return RedirectToPage("/Error");
			else
			{
				ParticipantsList? participant = await db.GetParticipantsByIdAsync((int)partId);

				if (participant == null)
					return NotFound();

				participant.PostNumbers.Add(post);

				await db.UpdateParticipantAsync(participant);
			}

			await GetPostsAsync((int)partId);
			return RedirectToPage();
		}

		public async Task GetPostsAsync(int partId)
		{
			List<Post> posts = await db.GetPostsAsync();

			ParticipantsList part = await db.GetParticipantsByIdAsync(partId);

			foreach (Post post in part.PostNumbers)
				posts.Remove(post);

			posts.Remove(await db.GetPostByIdAsync(3));

			var items = posts.Select(post => new SelectListItem
			{
				Text = post.Name.ToString(),
				Value = post.PostNumber.ToString()
			});

			ViewData["PostNames"] = items;
		}
	}
}
