using iAgenda.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAgenda.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Departments  { get; set; }
        public DbSet<Person> Persons { get; set; }

        public DbSet<ExternalContact> ExternalContacts { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<BranchOffice> BranchOffices { get; set; }

        public  DbSet<Driver> Drivers { get; set; }
    }
}
