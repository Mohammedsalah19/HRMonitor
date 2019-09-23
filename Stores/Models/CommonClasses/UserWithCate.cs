using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stores.Models.CommonClasses
{
    public class UserWithCate
    {
        public IEnumerable<Category> CategoryX { get; set; }
        public IEnumerable<UserAccess> userAccessX { get; set; }
        public IEnumerable<User> UserX { get; set; }
        public User userY { get; set; }
    }
}