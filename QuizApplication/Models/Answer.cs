namespace QuizApplication.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
    }
}
