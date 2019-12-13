using System.Collections.Generic;

namespace RetrospectiveDataApi.Models
{
    public class RetrospectiveData
    {

        public string Name { get; set; }
        public string Summary { get; set; }
        public string Date { get; set; } //pmalik - todo: change it to datetime
        public IList<Feedback> Feedback { get; set; }

    }
}