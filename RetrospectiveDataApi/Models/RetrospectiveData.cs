using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetrospectiveDataApi.Models
{
    public class RetrospectiveData
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please specify retrospective name")]
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Date { get; set; } //pmalik - todo: change it to datetime
        public IList<Feedback> Feedback { get; set; }

    }
}