using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApplication.Context;
using QuizApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace QuizApplication.Pages.Quiz
{
    public class ScoreHistoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public ScoreHistoryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<UserScore> UserScores { get; set; }  // Property to hold user scores
        public Dictionary<int, string> QuizTitles { get; set; } // Dictionary to hold QuizId and corresponding titles

        public async Task OnGetAsync()
        {
            UserScores = await _context.UserScores.ToListAsync(); // Retrieve all user scores from the database

            // Retrieve quiz titles into a dictionary
            QuizTitles = await _context.Quizs.ToDictionaryAsync(q => q.QuizId, q => q.Title);
        }
    }
}
