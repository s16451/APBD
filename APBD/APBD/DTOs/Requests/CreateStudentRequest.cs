using System;

namespace APBD.DTOs.Requests
{
    public class CreateStudentRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Password { get; set; }

        public DateTime GetBirthDate()
        {
            var split = BirthDate.Split('.');
            return new DateTime(int.Parse(split[2]), int.Parse(split[1]), int.Parse(split[0]));
        }
    }
}
