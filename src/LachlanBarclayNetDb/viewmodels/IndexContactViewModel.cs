using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace LachlanBarclayNet.ViewModel
{
    public class IndexContactViewModel
    {
        public List<YearPost> Years { get; set; }
        public string Category { get; set; }

        [Required]
        public string Name { get; set; }

        [Required,RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }

        public bool EmailSent { get; set; }
        
    }
}