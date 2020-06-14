using CodeFirst.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Models
{
    public class PrescriptionDbContext : DbContext
    {
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Medicament> Medicament { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicament { get; set; }

        public PrescriptionDbContext() { }

        public PrescriptionDbContext(DbContextOptions options)
        : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder )
        {
            base.OnModelCreating( modelBuilder );
            modelBuilder.ApplyConfiguration( new MedicamentEfConfig() );
            modelBuilder.ApplyConfiguration( new Prescription_MedicamentEfConfig() );
        }
    }
}
