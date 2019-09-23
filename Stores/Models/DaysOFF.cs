using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models
{
    public class DaysOFF
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("اسم القسم")]

        public string CategoryName { get; set; }
        [DisplayName("اسم الموظف")]

        public int empID { get; set; }
        [DisplayName("الرقم المحضر")]

        public string NumberOfMahdar { get; set; }
        [DisplayName("الرقم الوظيفى")]

        public string Number { get; set; }

        [DisplayName("من ")]

        public TimeSpan DateFrom { get; set; }
        [DisplayName(" الى")]

        public TimeSpan DateTo { get; set; }

        [DisplayName("تاريخ يوم عدم التواجد")]

        public DateTime DayofDate { get; set; }

        [DisplayName("تاريخ التقرير")]

        public DateTime reportDate  { get; set; }


    }
}