using System.Linq;
using Microsoft.AspNetCore.Mvc;
using APBD.Models;
using APBD.DTOs.Requests;

namespace APBD
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s16451;Integrated Security=True";
        private readonly IStudentDbService _service;

        public StudentsController(IStudentDbService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public IActionResult GetStudents()
        {
            var db = new s16451Context();
            return Ok(db.Student.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(string id)
        {
            var db = new s16451Context();
            var student = db.Student.Single(s => s.IndexNumber == id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(string id, UpdateStudentRequest request)
        {
            var db = new s16451Context();
            var student = new Student
            {
                IndexNumber = id
            };

            db.Attach( student );
            if ( request.IndexNumber != null ) {
                student.IndexNumber = request.IndexNumber;
                db.Entry( student ).Property( "IndexNumber" ).IsModified = true;
            }
            if ( request.FirstName != null ) {
                student.FirstName = request.FirstName;
                db.Entry( student ).Property( "FirstName" ).IsModified = true;
            }
            if ( request.LastName != null ) {
                student.FirstName = request.LastName;
                db.Entry( student ).Property( "LastName" ).IsModified = true;
            }
            if ( request.BirthDate != null ) {
                student.BirthDate = request.GetBirthDate();
                db.Entry( student ).Property( "BirthDate" ).IsModified = true;
            }
            if ( request.Password != null ) {
                student.Password = request.Password;
                db.Entry( student ).Property( "Password" ).IsModified = true;
            }
            if ( request.IdEnrollment != null ) {
                student.IdEnrollment = request.IdEnrollment.Value;
                db.Entry( student ).Property( "IdEnrollemnt" ).IsModified = true;
            }
            
            db.SaveChanges();

            return Ok("Aktualizacja ukończona");
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(string id)
        {
            var db = new s16451Context();
            var student = new Student
            {
                IndexNumber = id
            };
            db.Attach( student );
            db.Student.Remove( student );
            db.SaveChanges();

            return Ok("Usuwanie ukończone");
        }
    }
}