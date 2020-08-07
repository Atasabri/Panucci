using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panucci.Models
{
    public class DB : IdentityDbContext
    {
        
        public DB(DbContextOptions<DB> options) : base(options)
        {
        }

        public DbSet<Meal> Meals { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Subscriper> Subscripers { get; set; }

    }
}
