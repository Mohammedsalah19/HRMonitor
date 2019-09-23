using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models
{
    public class Employee
    {
        [Key]
        public int empID { get; set; }

        [DisplayName("اسم الموظف")]

        public string name { get; set; }
        [DisplayName("الرقم الوظيفى")]

        public string Number { get; set; }

        [DisplayName("القسم")]

        public int CategoryName { get; set; }
        [DisplayName("الدوام")]

        public string workHour { get; set; }
       // public string[] WorkHours = new[] { "7h", "6h" };
    }
}
