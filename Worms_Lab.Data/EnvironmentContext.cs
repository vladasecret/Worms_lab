using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Worms_lab.Data
{
    public class EnvironmentContext : DbContext
    {
        public DbSet<Behavior> Behaviors {get; set;}
        public DbSet<BehaviorStep> BehaviorSteps { get; set; }


        public EnvironmentContext(DbContextOptions<EnvironmentContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BehaviorConfig());
            modelBuilder.ApplyConfiguration(new BehaviorStepConfig());
        }
    }
}
