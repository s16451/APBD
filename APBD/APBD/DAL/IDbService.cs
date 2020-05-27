using System.Collections.Generic;
using APBD.Models.Previous;

namespace APBD
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
    }
}