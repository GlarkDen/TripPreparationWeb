using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MainPages.Models;
using System.ComponentModel.DataAnnotations;

namespace MainPages.Pages.Menu
{
    public class ChangePasswordModel : PageModel
    {
		private readonly Db db;

        public ChangePasswordModel()
        {
            db = new Db();
        }

        [BindProperty]
        public User User { get; set; }

		[BindProperty]
        [Required(ErrorMessage = "����������, ������� ������� ������")]
		public string OldPassword { get; set; } = "";

		[BindProperty]
        [Required(ErrorMessage = "����������, ��������� ������")]
        public string RepeatPassword { get; set; } = "";

        [BindProperty]
        public string CurrentPassword { get; set; } = "";

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
                CurrentPassword = user.Password;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
		{
            if (OldPassword != CurrentPassword)
            {
                ModelState.AddModelError("OldPassword", "�������� ������");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (RepeatPassword != User.Password)
            {
                ModelState.AddModelError("RepeatPassword", "������ �� ���������");
                return Page();
            }

			await db.UpdateUserAsync(User);

			TempData["Message"] = "������ ������� ������";
			return RedirectToPage("/Menu/Profile", new { id = User.UserNumber });
		}
	}
}
