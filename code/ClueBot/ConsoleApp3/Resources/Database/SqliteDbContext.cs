using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ClueBot.Resources.Database
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<Experience> ExperiencePoints { get; set; } //Experience table consists of many ExperiencePoints.

        protected override void OnConfiguring(DbContextOptionsBuilder Options)      //overrides the default configuration method - we want the database to be in a specific place
        {
            string DbLocation = Assembly.GetEntryAssembly().Location.Replace(@"bin\Debug\netcoreapp2.0", @"Data\Database.sqlite");  //specifies where the database is located - 
            Options.UseSqlite($"Data Source={DbLocation}Database.sqlite");      //the program now knows where to look for the database        the "@" means don't look for escape patterns - ignores backslashes
        }
    }
}
