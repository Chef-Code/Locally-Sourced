using LocallySourced.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace LocallySourced.DAL
{
    public class LSForumDB : DbContext
    {
        // Your context has been configured to use a 'LSForumDB' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'LocallySourced.DAL.LSForumDB' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'LSForumDB' 
        // connection string in the application configuration file.
        public LSForumDB()
            : base("name=LSForumDB")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.
        
        public DbSet<Member> Members { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}