using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Stores.Models.DAL
{
    public class ProjectContext:DbContext
    {

        public ProjectContext():base("DBHR")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<UserAccess> UserAccess { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<DaysOFF> DaysOFF { get; set; }
    }
}