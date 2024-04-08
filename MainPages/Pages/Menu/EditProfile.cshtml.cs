using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MainPages.Models;

namespace MainPages.Pages.Menu
{
    public class EditProfileModel : PageModel
    {
        private readonly Db db;

        public EditProfileModel()
        {
            db = new Db();
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            User? user = await db.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            User = user;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!await db.CheckUserByPhone(User.PhoneNumber, User.UserNumber))
            {
                ModelState.AddModelError("User.PhoneNumber", "Пользователь с указанным телефоном уже существует");
                return Page();
            }

            if (!await db.CheckUserByEmail(User.EmailAddress, User.UserNumber))
            {
                Console.WriteLine();
                ModelState.AddModelError("User.EmailAddress", "Пользователь с указанной почтой уже существует");
                return Page();
            }

            await db.UpdateUserAsync(User);

            TempData["Message"] = "Изменения успешно сохранены";
            return RedirectToPage("/Menu/Profile", new { id = User.UserNumber });
        }
    }
}
