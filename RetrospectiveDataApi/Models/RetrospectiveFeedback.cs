using System;
using System.Collections.Generic;

namespace RetrospectiveDataApi.Models
{
    public class RetrospectiveFeedback
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public DateTime Date { get; set; }

        public IList<string> Participants { get; set; }
        public IList<Feedback> Feedback { get; set; }

    }
}