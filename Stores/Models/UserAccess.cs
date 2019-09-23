using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models
{
    public class UserAccess
    {
         
        [Key]
        public int id { get; set; }

 
        public int userID { get; set; }
 
        public int cateID { get; set; }
     }
}