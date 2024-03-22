using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatGarden.Data.Models;

namespace CatGarden.Data.Configurations
{
    public class CatteryEntityConfiguration : IEntityTypeConfiguration<Cattery>
    {
        public void Configure(EntityTypeBuilder<Cattery> builder)
        {
            builder
                .HasOne(e => e.Owner)
                .WithMany(e => e.Catteries)
                .HasForeignKey(e => e.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
