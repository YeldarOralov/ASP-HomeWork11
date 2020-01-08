using HW11.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW11.DataAccess
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options):base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public object Request { get; internal set; }
    }
}
