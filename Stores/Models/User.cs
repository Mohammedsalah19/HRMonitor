using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("اسم المراقب")]
        [Required(ErrorMessage = "يجب الادخال")]
         public string name { get; set; }
        [DisplayName("الرقم الوظيفى")]
        [Required(ErrorMessage = "يجب الادخال")]

        public string Number { get; set; }
        [DisplayName("كلمه المرور")]

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("حالة حساب المراقب")]

        public bool Status { get; set; }
        [DisplayName("السماح له بصلاحيات الادمن")]

        public bool AsAdmin { get; set; }
    }
}