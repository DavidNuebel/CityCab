using CityCab.Libraries.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityCab.Libraries.Database.Context
{
    public class CityCabServiceContext : DbContext
    {
        public CityCabServiceContext(DbContextOptions options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Job> Jobs { get; set; }
    }
}
