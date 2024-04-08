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
    public class DeleteProfileModel : PageModel
    {
        private readonly Db db;

        public DeleteProfileModel()
        {
            db = new Db();
        }

        [BindProperty]
        public User User { get; set; } = default!;

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

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var user = await db.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();
            else
            {
                await db.DeleteUserAsync(user);
                return RedirectToPage("/Index");
            }
        }
    }
}
