using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApplication.Context;
using Microsoft.EntityFrameworkCore;

namespace QuizApplication.Pages.Question
{
    public class ManagerModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // Inject the DbContext through the constructor
        public ManagerModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Property to hold the list of questions
        public List<Models.Question> Questions { get; set; }

        // OnGet method to load questions from the database
        public async Task OnGetAsync()
        {
            // Fetch the questions from the database, including the related answers
            Questions = await _context.Questions
                                      .Include(q => q.Answers) // Include related answers
                                      .ToListAsync();
        }
    }
}
