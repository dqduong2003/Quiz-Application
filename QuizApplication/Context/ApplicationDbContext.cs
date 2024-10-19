using Microsoft.EntityFrameworkCore;

namespace QuizApplication.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}
        public DbSet<Models.Question> Questions { get; set; }
        public DbSet<Models.Answer> Answers { get; set; }
        public DbSet<Models.Quiz> Quizs { get; set; }
        public DbSet<Models.UserScore> UserScores { get; set; }
    }
}
