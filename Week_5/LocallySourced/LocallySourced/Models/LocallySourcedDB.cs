using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LocallySourced.Models
{
    public class LocallySourcedDB : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public LocallySourcedDB() : base("name=LocallySourcedDB")
        {
        }

        public System.Data.Entity.DbSet<LocallySourced.Models.Member> Members { get; set; }

        public System.Data.Entity.DbSet<LocallySourced.Models.Topic> Topics { get; set; }

        public System.Data.Entity.DbSet<LocallySourced.Models.Message> Messages { get; set; }

        //Information provided below by: LONNIE TETER
        //Documentation in regards to the DBModelBuilder and using Fluent API can be found at
        //http://www.entityframeworktutorial.net/code-first/fluent-api-in-code-first.aspx
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure domain classes using modelBuilder here:

            
            //NOTICE!! you must use either config1 or config2 but NOT!! both
            // Configure EntityID as PK for EntityProperty <<-- Can be Used with both config1 and config2 shown below
            //  modelBuilder.Entity<EntityProperty>()
            //              .HasKey(e => e.EntityID);

            // Config1: EntityID as FK for EntityProperty
            // modelBuilder.Entity<Entity>()
            //             .HasOptional(c => c.EntityProperty)   // ALLOW EntityProperty to be OPTIONAL for Entity
            //             .WithRequired(ce => ce.Entity);       // Create inverse relationship

            // Config2: EntityID as FK for EntityProperty
            //modelBuilder.Entity<EntityProperty>()
            //            .HasRequired(ce => ce.Entity)          // FORCE EntityProperty to be REQUIRED for Entity
            //            .WithOptional(c => c.EntityProperty); 

            base.OnModelCreating(modelBuilder);
        }
    }
}
