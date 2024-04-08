using MainPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MainPages.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly Db db;

        public LoginModel()
        {
            db = new Db();
        }

        public void OnGet()
        {

        }

        [BindProperty]
        public string Email { get; set; } = "";

		[BindProperty]
		public string Password { get; set; } = "";

		public async Task<IActionResult> OnPostAsync() 
        {
            User? user = await db.GetUserByEmailAsync(Email);

            if (user == null)
            {
				TempData["Error"] = "Пользователя с указанной почтой не существует";
				return Page();
            }

            if (user.Password == Password)
            {
				return RedirectToPage("/Menu/Trips", new { id = user.UserNumber });
			}
            else
            {
				TempData["Error"] = "Неправильный пароль!";
			}

            return Page();
		}
    }
}
