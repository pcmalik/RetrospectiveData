using RetrospectiveDataApi.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetrospectiveDataApi.Models
{
    public class Retrospective
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please specify retrospective name")]
        public string Name { get; set; }
        public string Summary { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = RetrospectiveConstants.DATE_VALIDATION_MESSAGE)]
        [DataType(DataType.Date)]
        public string Date { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please specify participants")]
        public IList<string> Participants { get; set; }

    }
}