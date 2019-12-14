using System.ComponentModel.DataAnnotations;

namespace RetrospectiveDataApi.Models
{
    public class Feedback
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please specify feedback name")]
        public string Name { get; set; }
        public string Body { get; set; }
        public FeedbackType FeedbackType { get; set; }

    }
}