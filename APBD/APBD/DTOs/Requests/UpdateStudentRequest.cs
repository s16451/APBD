using System;
using System.ComponentModel.DataAnnotations;

namespace APBD.DTOs.Requests
{
    public class UpdateStudentRequest
    {
        [Required]
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Password { get; set; }
        public int? IdEnrollment { get; set; }

        public DateTime GetBirthDate()
        {
            var split = BirthDate.Split('.');
            return new DateTime( int.Parse( split[2] ), int.Parse( split[1] ), int.Parse( split[0] ) );
        }
    }
}
