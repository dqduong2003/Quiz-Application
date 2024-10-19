using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuizApplication.Context;
using QuizApplication.Models;

namespace QuizApplication.Pages.Quiz
{
    public class TakeModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public TakeModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Models.Quiz Quiz { get; set; }
        public List<Models.Question> Questions { get; set; }

        // GET method to load the quiz and its questions
        public async Task<IActionResult> OnGetAsync(int quizId)
        {
            Quiz = await _context.Quizs.FindAsync(quizId);
            if (Quiz == null)
            {
                return NotFound();
            }
            Console.WriteLine(quizId);

            Questions = await _context.Questions
                .Where(q => q.QuizId == quizId)
                .Include(q => q.Answers) // Make sure to include answers
                .ToListAsync();

            return Page();
        }

        // POST method to handle quiz submission
        public async Task<IActionResult> OnPostAsync(int quizId)
        {
            // Logic for processing the submitted answers
            Quiz = await _context.Quizs.FindAsync(quizId);
            if (Quiz == null)
            {
                return NotFound();
            }

            Questions = await _context.Questions
                .Where(q => q.QuizId == quizId)
                .Include(q => q.Answers) // Include answers for each question
                .ToListAsync();

            var score = 0;
            var selectedAnswers = new Dictionary<int, int?>(); // Store question ID and selected answer ID

            // Iterate over the submitted answers
            foreach (var question in Questions)
            {
                // Get the user's answer from the posted form
                var selectedAnswerId = Request.Form[$"Question_{question.QuestionId}"];

                if (!string.IsNullOrEmpty(selectedAnswerId) && int.TryParse(selectedAnswerId, out int selectedId))
                {
                    // Add the user's answer to the dictionary
                    selectedAnswers[question.QuestionId] = selectedId;

                    // Check if the selected answer is correct
                    if (selectedId == question.CorrectAnswerId)
                    {
                        score++; // Increment score for correct answer
                    }
                }
                else
                {
                    // If no answer is selected, mark as null
                    selectedAnswers[question.QuestionId] = null;
                }
            }
            TempData.Put("SelectedAnswers", selectedAnswers);


            // Add the score to the UserScores
            var newScore = new Models.UserScore
            {
                QuizId = Quiz.QuizId,
                Score = score,
                Timestamp = DateTime.UtcNow
            };

            _context.UserScores.Add(newScore);
            await _context.SaveChangesAsync();  

            return RedirectToPage("/Quiz/Result", new { quizId, score });
        }
    }
}
