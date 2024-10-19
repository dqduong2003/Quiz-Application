using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApplication.Context;
using QuizApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace QuizApplication.Pages.Question
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject DbContext
        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string QuestionText { get; set; }

        [BindProperty]
        public List<string> AnswerTexts { get; set; } = new List<string>() { "", "", "", "" };

        [BindProperty]
        public int CorrectAnswerIndex { get; set; }

        [BindProperty]
        public int QuizID { get; set; }  // Added QuizID property

        public List<Models.Quiz> Quizzes { get; set; }  // Property to hold available quizzes

        // GET method to show the form
        public async Task OnGetAsync()
        {
            // Load available quizzes from the database
            Quizzes = await _context.Quizs.ToListAsync();  // Assuming you have a Quizzes DbSet
        }

        // POST method to create a new question and save to the database
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Create the new question without assigning CorrectAnswerId for now
            var newQuestion = new Models.Question
            {
                Text = QuestionText,
                Answers = AnswerTexts.Select(answerText => new Answer
                {
                    Text = answerText
                }).ToList(),
                QuizId = QuizID  // Set the QuizID
            };

            // Add the new question (along with answers) to the database
            _context.Questions.Add(newQuestion);
            await _context.SaveChangesAsync();  // Save to generate IDs

            // Now that the answers have been saved and have IDs, update the CorrectAnswerId
            var correctAnswer = newQuestion.Answers[CorrectAnswerIndex];  // Find the correct answer from the list
            newQuestion.CorrectAnswerId = correctAnswer.AnswerId;

            // Save the updated question with the correct CorrectAnswerId
            await _context.SaveChangesAsync();

            return RedirectToPage("/Question/Manager");
        }
    }
}
