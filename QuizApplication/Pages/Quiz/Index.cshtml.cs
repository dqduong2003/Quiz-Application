using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuizApplication.Context;
using QuizApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizApplication.Pages.Quiz
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Models.Quiz> Quizzes { get; set; }

        public async Task OnGetAsync()
        {
            Quizzes = await _context.Quizs
                                .Include(q => q.Questions)  // Ensure questions are loaded
                                .ToListAsync();
        }
    }
}
