using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public DetailsModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public Student Student { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id == 0)
            {
                return NotFound();
            }

            Student = await _context.Students
                .Include(s=>s.Enrollments)
                .ThenInclude(e=>e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == Id);
            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
