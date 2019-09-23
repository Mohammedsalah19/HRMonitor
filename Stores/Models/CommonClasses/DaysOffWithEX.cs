using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stores.Models.CommonClasses
{
    public class DaysOffWithEX
    {
        public IEnumerable<Category> CategoryX { get; set; }
        public  Employee EmpY { get; set; }
        public     IEnumerable<Employee> EmpX { get; set; }
        public  DaysOFF DaysOffY { get; set; }
    }
}