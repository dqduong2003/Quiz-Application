using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApplication.Context;
using QuizApplication.Models;
using System.Threading.Tasks;

namespace QuizApplication.Pages.Quiz
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Description { get; set; }

        // GET method (optional)
        public void OnGet()
        {
        }

        // POST method to handle form submission
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Create a new quiz
            var newQuiz = new Models.Quiz
            {
                Title = Title,
                Description = Description
            };

            // Add the new quiz to the database
            _context.Quizs.Add(newQuiz);
            await _context.SaveChangesAsync();

            // Redirect back to the quiz list
            return RedirectToPage("/Quiz/Index");
        }
    }
}
