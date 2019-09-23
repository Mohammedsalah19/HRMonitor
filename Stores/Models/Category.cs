using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models
{
    public class Category
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "يجب الادخال")]

        [DisplayName("اسم القسم")]

        public string CategoryName { get; set; }
        [DisplayName("اسم مدير القسم")]
        [Required(ErrorMessage = "يجب الادخال")]

        public string ManagerName { get; set; }
        [DisplayName("حالة القسم")]

        public bool Status { get; set; }
    }
}