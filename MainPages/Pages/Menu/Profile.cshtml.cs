using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MainPages.Models;
using System.ComponentModel.DataAnnotations;

namespace MainPages.Pages.Menu
{
	public class ProfileModel : PageModel
	{
		private readonly Db db;

		public ProfileModel()
		{
			db = new Db();
        }

		public User User { get; set; } = default!;

		public bool CanDeleteProfile = true;

		public async Task<IActionResult> OnGetAsync(int id)
		{
            var user = await db.GetUserByIdAsync(id);

			if (user == null)
			{
				return NotFound();
			}
			else
			{
				User = user;
            }

			if (User.ParticipantsLists.Where(p => DataChecker.CheckDateNow(p.TripNumberNavigation.StartDate) != -1)
				.SelectMany(p => p.PostNumbers).FirstOrDefault(p => p.Name == "Руководитель") != default)
				CanDeleteProfile = false;

            return Page();
		}
	}
}
