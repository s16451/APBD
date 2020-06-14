﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public int IdPatient { get; set; }

        [Required, MaxLength( 100 )]
        public string FirstName { get; set; }

        [Required, MaxLength( 100 )]
        public string LastName { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
