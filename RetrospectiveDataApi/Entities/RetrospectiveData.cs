using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetrospectiveDataApi.Entities
{
    public class RetrospectiveData
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public DateTime Date { get; set; }

        public IList<string> Participants { get; set; }
        public IList<Feedback> Feedback { get; set; }

    }
}