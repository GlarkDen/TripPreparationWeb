using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MainPages.Models;
using System.Collections;

namespace MainPages.Pages.Menu
{
    public class TripsModel : PageModel
    {
        private readonly Db db;

        public TripsModel()
        {
            db = new Db();
        }

        public IList<TripsList> TripsList { get;set; } = default!;

        public User User { get; set; }

		public List<int> UserGeneralPosts { get; set; }

        public async Task OnGetAsync(int id)
        {
            User? user = await db.GetUserByIdAsync(id);

            if (user == null)
            {
                RedirectToPage("/Error");
            }
            else
            {
                User = user;
            }

			TripsList = User.ParticipantsLists.Select(p => p.TripNumberNavigation).ToList();

            UserGeneralPosts = User.ParticipantsLists.Where(p => p.PostNumbers.Select(p => p.Name).Contains("Руководитель")).Select(p => p.TripNumber).ToList();
		}
    }
}
