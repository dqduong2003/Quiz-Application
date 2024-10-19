using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using QuizApplication.Context;

using Newtonsoft.Json;

namespace QuizApplication.Pages.Quiz
{
    public static class TempDataExtensions
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            tempData.TryGetValue(key, out var o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
    }

    public class ResultModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ResultModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Models.Quiz Quiz { get; set; }
        public List<Models.Question> Questions { get; set; }
        public Dictionary<int, int?> SelectedAnswers { get; set; } = new Dictionary<int, int?>(); // To store selected answers
        public int Score { get; set; }

        // OnGet method to display the results
        public async Task<IActionResult> OnGetAsync(int quizId, int score)
        {
            Quiz = await _context.Quizs.FindAsync(quizId);
            if (Quiz == null)
            {
                return NotFound();
            }

            // Load all questions and answers for the quiz
            Questions = await _context.Questions
                .Where(q => q.QuizId == quizId)
                .Include(q => q.Answers)
                .ToListAsync();

            // Retrieve selected answers from the TempData or other storage
            if (TempData.ContainsKey("SelectedAnswers"))
            {
                SelectedAnswers = TempData.Get<Dictionary<int, int?>>(("SelectedAnswers"));
            }

            Score = score; // Assign the score passed from the Take page
            return Page();
        }
    }
}