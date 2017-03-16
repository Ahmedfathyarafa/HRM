using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRM.Web.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}