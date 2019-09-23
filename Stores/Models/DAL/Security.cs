using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stores.Models.DAL
{
    public class Security
    {
        ProjectContext _db = new ProjectContext();

        public bool AsAdmin()
        {
            if (HttpContext.Current.Session["userName"] != null)
            {
                string session = HttpContext.Current.Session["userID"].ToString();
                // bool data = _db.UserAccess.Where(p => p.userID.ToString() == session).Select(f => f.users).FirstOrDefault();
                bool user = _db.Users.Where(f => f.Id.ToString() == session).Select(f => f.AsAdmin).FirstOrDefault();

                if (user == true)
                {
                    return true;
                }
            }
            return false;
        }


        public bool AsUser()
        {
            if (HttpContext.Current.Session["userName"] != null)
            {

                return true;

            }
            return false;
        }


    }
}