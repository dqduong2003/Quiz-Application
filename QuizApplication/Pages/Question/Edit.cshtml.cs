using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApplication.Context;
using QuizApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace QuizApplication.Pages.Question
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject DbContext
        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Question Question { get; set; }

        // GET method to retrieve the question and answers
        public async Task<IActionResult> OnGetAsync(int id) 
        {
            Question = await _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.QuestionId == id);

            if (Question == null)
            {
                return NotFound();
            }

            return Page();
        }

        // POST method to update the question
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Retrieve the existing question from the database
            var existingQuestion = await _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.QuestionId == Question.QuestionId);

            if (existingQuestion == null)
            {
                return NotFound();
            }

            // Update properties of the existing question
            existingQuestion.Text = Question.Text;
            existingQuestion.CorrectAnswerId = Question.CorrectAnswerId;

            // Update answers
            for (int i = 0; i < Question.Answers.Count; i++)
            {
                var answer = existingQuestion.Answers.FirstOrDefault(a => a.AnswerId == Question.Answers[i].AnswerId);
                if (answer != null)
                {
                    answer.Text = Question.Answers[i].Text;
                }
            }

            await _context.SaveChangesAsync();
           

            return RedirectToPage("/Question/Manager");
        }
    }
}
