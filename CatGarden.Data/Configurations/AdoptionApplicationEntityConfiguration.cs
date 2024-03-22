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
    public class AdoptionApplicationEntityConfiguration : IEntityTypeConfiguration<AdoptionApplication>
    {
        public void Configure(EntityTypeBuilder<AdoptionApplication> builder)
        {
            builder
                .HasOne(a => a.Cat)
                .WithMany(a => a.AdoptionApplications)
                .HasForeignKey(a => a.CatId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
