using DOMAIN;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace API.Context
{
    public class APIContext : DbContext
    {
        public APIContext() : base("name = MyContextDB")
        {

        }

        public DbSet<Product> Products { get; set; }

    }
}