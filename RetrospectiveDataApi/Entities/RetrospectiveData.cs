using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetrospectiveDataApi.Entities
{
    public class RetrospectiveData
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please specify retrospective name")]
        public string Name { get; set; }
        public string Summary { get; set; }
        public DateTime Date { get; set; }
        public IList<Feedback> Feedback { get; set; }

    }
}