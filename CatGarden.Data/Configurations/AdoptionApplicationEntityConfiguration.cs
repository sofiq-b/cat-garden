using CatGarden.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatGarden.Data.Configurations
{
    public class AdoptionApplicationEntityConfiguration : IEntityTypeConfiguration<AdoptionApplication>
    {
        public void Configure(EntityTypeBuilder<AdoptionApplication> builder)
        {
            builder
                .HasOne(app => app.Cat)     
                .WithMany(cat => cat.AdoptionApplications)                     
                .HasForeignKey(app => app.CatId)             
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
