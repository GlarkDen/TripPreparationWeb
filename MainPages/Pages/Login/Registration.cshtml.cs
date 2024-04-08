using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MainPages.Models;
using System.ComponentModel.DataAnnotations;

namespace MainPages.Pages.Login
{
    public class RegistrationModel : PageModel
    {
        private readonly Db db;

        public RegistrationModel()
        {
            db = new Db();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;

        [BindProperty]
        [Required(ErrorMessage = "Пожалуйста, повторите пароль")]
        public string RepeatPassword { get; set; } = "";

		public async Task<IActionResult> OnPostAsync()
        {
			if (RepeatPassword != User.Password)
			{
				ModelState.AddModelError("RepeatPassword", "Пароли не совпадают");
				return Page();
			}

			if (await db.GetUserByPhone(User.PhoneNumber) != null)
			{
				ModelState.AddModelError("User.PhoneNumber", "Пользователь с указанным телефоном уже существует");
				return Page();
			}

			if (await db.GetUserByEmailAsync(User.EmailAddress) != null)
			{
				Console.WriteLine();
				ModelState.AddModelError("User.EmailAddress", "Пользователь с указанной почтой уже существует");
				return Page();
			}

			await db.AddUserAsync(User);
			return RedirectToPage("/Menu/Trips", new { id = await db.GetMaxUserIdAsync() });
		}
    }
}
