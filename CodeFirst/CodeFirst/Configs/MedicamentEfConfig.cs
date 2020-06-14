using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeFirst.Configs
{
    public class MedicamentEfConfig : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder )
        {
            builder.HasKey( e => e.IdMedicament );
            builder.Property( e => e.Name ).HasMaxLength( 100 ).IsRequired();
            builder.Property( e => e.Description ).HasMaxLength( 100 ).IsRequired();
            builder.Property( e => e.Type ).HasMaxLength( 100 ).IsRequired();
        }
    }
}
