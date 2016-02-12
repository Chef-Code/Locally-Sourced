using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using LocallySourced.Models;

namespace LocallySourced.DAL
{
    public class LSForumContext : DbContext
    {

        public LSForumContext() : base("LSForumContext")
        {
        }
        
        public DbSet<Member> Members { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}