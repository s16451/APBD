using System;
using System.Linq;
using APBD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APBD
{
    [ApiController]
    [Route("api/enrollments")]
    //[Authorize(Roles = "employee")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IStudentDbService _service;

        public EnrollmentsController(IStudentDbService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            var db = new s16451Context();
            var studies = db.Studies.SingleOrDefault(s => s.Name==request.Studies);
            if(studies == null ) {
                return BadRequest( "Studia nie istnieja" );
            }
            var enrollment = db.Enrollment
                .SingleOrDefault(e => e.IdStudy==studies.IdStudy && e.Semester==1);
            if ( enrollment == null ) {
                enrollment = new Enrollment
                {
                    Semester = 1,
                    IdStudy = studies.IdStudy,
                    StartDate = DateTime.Now
                };
                db.Enrollment.Add( enrollment );
                db.SaveChanges();
            }
            if (db.Student.Any( s => s.IndexNumber == request.IndexNumber)){
                return BadRequest( "Student id jest zajete przez innego studenta" );
            }

            var student = new Student
            {
                IndexNumber = request.IndexNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = MapStringToDateTime(request.Birthdate),
                IdEnrollment = enrollment.IdEnrollment
            };
            db.Student.Add( student );
            db.SaveChanges();

            return Ok(student);
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudents(PromoteStudentsRequest request)
        {
            var db = new s16451Context();
            var param1 = new SqlParameter("Studies", request.Studies);
            var param2 = new SqlParameter("Semester", request.Semester);
            try {
                var affected = db.Database
                    .ExecuteSqlRaw("EXEC PromoteStudents @Studies, @Semester", param1, param2);
                return Ok( $"Promoted {affected} students" );
            } catch(Exception e ) {
                return NotFound( e.Message );
            }
        }

        private DateTime MapStringToDateTime( string date )
        {
            var splitDate = date.Split('.');
            return new DateTime( int.Parse( splitDate[2] ), int.Parse( splitDate[1] ), int.Parse( splitDate[0] ) );
        }
    }
}