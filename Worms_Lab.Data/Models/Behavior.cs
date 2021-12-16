using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Worms_lab.Data
{
    public class Behavior
    {
        
        public int Id { get; set; }
        
        public string Name { get; set; }

        public List<BehaviorStep> BehaviorSteps { get; set; }

    }

    class BehaviorConfig : IEntityTypeConfiguration<Behavior>
    {
        public void Configure(EntityTypeBuilder<Behavior> builder)
        {
            builder.HasIndex(b => b.Name).IsUnique();
            builder.Property(b => b.Name).IsRequired();
            builder.HasMany(b => b.BehaviorSteps).WithOne().HasForeignKey(bs => bs.BehaviorId);
        }
    }

}
