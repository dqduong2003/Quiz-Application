
namespace QuizApplication.Models
{
    public class UserScore
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public int Score { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
