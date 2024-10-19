using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApplication.Context;
using Microsoft.EntityFrameworkCore;

namespace QuizApplication.Pages.Question
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Question Question { get; set; }

        // GET method to load the question to be deleted
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Question = await _context.Questions
                .Include(q => q.Answers)  // Load answers if needed
                .FirstOrDefaultAsync(q => q.QuestionId == id);

            if (Question == null)
            {
                return NotFound();
            }

            return Page();
        }

        // POST method to delete the question
        public async Task<IActionResult> OnPostAsync()
        {
            if (Question == null)
            {
                return NotFound();
            }

            // Remove the question from the database
            _context.Questions.Remove(Question);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Question/Manager");  // Redirect after deletion
        }
    }
}
