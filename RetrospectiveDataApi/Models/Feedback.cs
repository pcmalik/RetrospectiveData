namespace RetrospectiveDataApi.Models
{
    public class Feedback
    {
        public string Name { get; set; }
        public string Body { get; set; }
        public FeedbackType FeedbackType { get; set; }

    }
}