using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeFirst.Configs
{
    public class Prescription_MedicamentEfConfig : IEntityTypeConfiguration<Prescription_Medicament>
    {
        public void Configure( EntityTypeBuilder<Prescription_Medicament> builder )
        {
            builder.HasKey( e => new { e.IdMedicament, e.IdPrescription } );

            builder
                .HasOne( e => e.Medicament )
                .WithMany( e => e.Prescription_Medicaments )
                .HasForeignKey( e => e.IdMedicament );

            builder
                .HasOne( e => e.Prescription )
                .WithMany( e => e.Prescription_Medicaments )
                .HasForeignKey( e => e.IdPrescription );

            builder.Property( e => e.Details ).HasMaxLength( 100 ).IsRequired();
        }
    }
}
