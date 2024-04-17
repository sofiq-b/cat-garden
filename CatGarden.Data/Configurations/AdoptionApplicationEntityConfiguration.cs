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
                .HasOne(app => app.Cat)     // A Cat can have multiple AdoptionApplications
                .WithMany(cat => cat.AdoptionApplications)                      // An AdoptionApplication belongs to one Cat
                .HasForeignKey(app => app.CatId)             // Foreign key property in AdoptionApplication entity
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
