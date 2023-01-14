using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InteractiveSpaces.Models;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;

namespace InteractiveSpaces.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext (DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Activity2User>().HasKey(a => new { a.ActivityId, a.User });

            builder.Entity<Activity>().HasAlternateKey(e=>e.Name);
            
            
            builder.Entity<Entity>().HasAlternateKey(e => e.Name);
            builder.Entity<Entity>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Entity3D>("Entity3D");

            builder.Entity<ActionEntityStep>().HasOne(a => a.EntityStep)
                .WithMany(e => e.HasActions).OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<EntityStep>().HasOne(es=>es.Entity).WithMany();

            builder.Entity<EntityStep>().HasAlternateKey(e => new {e.EntityId,e.StepDescriptionId });

            //entity and location have a one to one relationship, so they share table
            //https://learn.microsoft.com/en-us/ef/core/modeling/table-splitting
            builder.Entity<Location>(
            dob =>
            {
                dob.ToTable("EntityStep");
            });
            builder.Entity<Location>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<LocationGPS>("LocationGPS")
                .HasValue<Location3D>("Location3D");

            builder.Entity<EntityStep>(
                ob =>
                {
                    ob.ToTable("EntityStep");
                    ob.HasOne(o => o.LocatedIn).WithOne()
                        .HasForeignKey<Location>(o => o.Id);
                    ob.Navigation(o => o.LocatedIn).IsRequired();
                });



            //the id of the animation in the prefab is unique
            builder.Entity<Animation>().HasAlternateKey(a=> new {a.Id});

            builder.Entity<InteractiveSpace>().HasAlternateKey(e => new { e.Name, e.Owner });
            builder.Entity<InteractiveSpace>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<InteractiveSpace>("InteractiveSpace")
                .HasValue<InteractiveSpace3D>("InteractiveSpace3D");

            builder.Entity<Resource>().HasAlternateKey(e => e.Name);

            builder.Entity<Location>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<LocationGPS>("LocationGPS")
                .HasValue<Location3D>("Location3D");

            builder.Entity<Step>().HasOne(s => s.NextStep)
                .WithOne(s => s.PreviousStep);

            //Step---->>>>Activity
            builder.Entity<Step>().HasOne(s => s.Activity)
                .WithMany(a=>a.Steps).OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<StepDescription>().HasOne(s => s.Step)
                .WithMany(a => a.StepDescriptions).OnDelete(DeleteBehavior.ClientCascade);


            builder.Entity<EntityStep>().HasOne(es => es.StepDescription)
                .WithMany(a => a.EntityStep).OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<ActionEntityStep>().HasOne(aes => aes.EntityStep)
                .WithMany(es=>es.HasActions).OnDelete(DeleteBehavior.ClientCascade);

            //Activity---->FirstStep
            builder.Entity<Activity>().HasOne(a => a.FirstStep).WithOne();
            //    .HasForeignKey<Step>("Step_FK").OnDelete(DeleteBehavior.NoAction);

          

  
        }

        public DbSet<InteractiveSpaces.Models.Activity> Activity { get; set; } = default!;

        public DbSet<InteractiveSpaces.Models.Resource> Resource { get; set; }

        public DbSet<InteractiveSpaces.Models.Entity3D> Entity3d { get; set; }
        public DbSet<InteractiveSpaces.Models.Animation> Animation { get; set; }

        public DbSet<InteractiveSpaces.Models.InteractiveSpace3D> InteractiveSpace3D { get; set; }

        public DbSet<InteractiveSpaces.Models.Step> Step { get; set; }
        public DbSet<InteractiveSpaces.Models.StepDescription> StepDescription { get; set; }
        

        public DbSet<InteractiveSpaces.Models.ActionEntityStep> ActionEntityStep { get; set; }

        public DbSet<InteractiveSpaces.Models.EntityStep> EntityStep { get; set; }

        public DbSet<InteractiveSpaces.Models.Activity2User> Activity2User { get; set; }


    }
}
