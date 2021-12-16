using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worms_lab.Data
{
    public class BehaviorStep
    {
        public int Id { get; set; }
        public int BehaviorId { get; set; }
        public int StepNum { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }



    class BehaviorStepConfig : IEntityTypeConfiguration<BehaviorStep>
    {
        public void Configure(EntityTypeBuilder<BehaviorStep> builder)
        {
            builder.HasIndex(b => new {b.BehaviorId, b.StepNum}).IsUnique();
            
        }
    }
}
