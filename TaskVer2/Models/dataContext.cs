using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TaskVer2.Models
{
    public class dataContext : DbContext
    {
        public DbSet<Chanel> Chanel { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}