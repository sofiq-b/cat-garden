using CatGarden.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;
using static CatGarden.Common.Enums;

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

            builder.HasData(this.GenerateCatteries());
        }

        private Cattery[] GenerateCatteries()
        {
            ICollection<Cattery> catteries = new HashSet<Cattery>();

            Cattery cattery;
            cattery = new Cattery()
            {
                OwnerId = Guid.Parse("b1bfe4d3-a412-4ffe-b066-fc04238e432b"),
                Name = "Whisker Haven",
                City = City.Sofia,
                Address = "5, Inzh. Georgi Belov",
                EstablishmentDate = DateTime.ParseExact("19/01/2012", "dd/MM/yyyy", CultureInfo.InvariantCulture)
            };
            catteries.Add(cattery);

            cattery = new Cattery()
            {
                OwnerId = Guid.Parse("b1bfe4d3-a412-4ffe-b066-fc04238e432b"),
                Name = "Purrfect Paws",
                City = City.Varna,
                Address = "29, Sevastokrator Kaloyan",
                EstablishmentDate = DateTime.ParseExact("11/02/2006", "dd/MM/yyyy", CultureInfo.InvariantCulture)
            };
            catteries.Add(cattery);

            return catteries.ToArray();
        }
    }
}
