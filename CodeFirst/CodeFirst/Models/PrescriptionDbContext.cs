using CodeFirst.Configs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

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
            SeedDatabase(modelBuilder);
        }

        private void SeedDatabase(ModelBuilder modelBuilder )
        {
            var Doctors = new List<Doctor>();
            var Prescriptions = new List<Prescription>();
            var Patients = new List<Patient>();
            var PrescriptionMedicaments = new List<Prescription_Medicament>();
            var Medicaments = new List<Medicament>();

            Doctors.Add( new Doctor { IdDoctor = 1, FirstName = "Adam", LastName = "Kowalski", Email = "doc1@gmail.com" } );
            Doctors.Add( new Doctor { IdDoctor = 2, FirstName = "Ewa", LastName = "Grodek", Email = "doc2@gmail.com" } );
            Doctors.Add( new Doctor { IdDoctor = 3, FirstName = "Janusz", LastName = "Polak", Email = "doc3@gmail.com" } );

            Patients.Add( new Patient { IdPatient = 1, FirstName = "Mateusz", LastName = "Piaseczny", Birthdate = new DateTime(1990, 10, 5) } );
            Patients.Add( new Patient { IdPatient = 2, FirstName = "Ala", LastName = "Tarska", Birthdate = new DateTime( 1991, 2, 16 ) } );
            Patients.Add( new Patient { IdPatient = 3, FirstName = "Franek", LastName = "Zapalski", Birthdate = new DateTime( 1993, 3, 18 ) } );

            Prescriptions.Add( new Prescription { IdPrescription = 1, IdDoctor = 1, IdPatient = 1, Date = new DateTime(), DueDate = new DateTime()} );
            Prescriptions.Add( new Prescription { IdPrescription = 2, IdDoctor = 2, IdPatient = 2, Date = new DateTime(), DueDate = new DateTime()} );
            Prescriptions.Add( new Prescription { IdPrescription = 3, IdDoctor = 3, IdPatient = 3, Date = new DateTime(), DueDate = new DateTime()} );

            Medicaments.Add( new Medicament { IdMedicament = 1, Name = "Med1", Description = "Desc1", Type = "Type1" } );
            Medicaments.Add( new Medicament { IdMedicament = 2, Name = "Med2", Description = "Desc2", Type = "Type2" } );
            Medicaments.Add( new Medicament { IdMedicament = 3, Name = "Med3", Description = "Desc3", Type = "Type3" } );

            PrescriptionMedicaments.Add( new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1, Dose = 1, Details = "None" } );
            PrescriptionMedicaments.Add( new Prescription_Medicament { IdMedicament = 2, IdPrescription = 2, Dose = 10, Details = "None" } );
            PrescriptionMedicaments.Add( new Prescription_Medicament { IdMedicament = 3, IdPrescription = 3, Dose = 100, Details = "None" } );

            modelBuilder.Entity<Doctor>().HasData( Doctors );
            modelBuilder.Entity<Patient>().HasData( Patients );
            modelBuilder.Entity<Prescription>().HasData( Prescriptions );
            modelBuilder.Entity<Medicament>().HasData( Medicaments );
            modelBuilder.Entity<Prescription_Medicament>().HasData( PrescriptionMedicaments );
        }
    }
}
