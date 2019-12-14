using RetrospectiveDataApi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetrospectiveDataApi.Models
{
    public class RetrospectiveDataModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please specify retrospective name")]
        public string Name { get; set; }
        public string Summary { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please specify valid date value in dd-mm-yyyy format")]
        [DataType(DataType.Date)]
        public string Date { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please specify participants")]
        public IList<string> Participants { get; set; }

    }
}