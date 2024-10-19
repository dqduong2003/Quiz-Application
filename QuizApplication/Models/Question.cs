namespace QuizApplication.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int QuizId { get; set; }
        public List<Answer> Answers { get; set; }
        public int CorrectAnswerId { get; set; }
    }
}
