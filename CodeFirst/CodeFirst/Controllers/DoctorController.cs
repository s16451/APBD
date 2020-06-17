using System.Linq;
using CodeFirst.DTOs.Requests;
using CodeFirst.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Controllers
{
    [Route( "api/doctors" )]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly PrescriptionDbContext _context;

        public DoctorController(PrescriptionDbContext context )
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDoctors()
        {
            return Ok(_context.Doctor.ToList());
        }

        [HttpGet( "{id}" )]
        public IActionResult GetDoctor( int id )
        {
            var doctor = _context.Doctor.Single(d => d.IdDoctor == id);
            if ( doctor == null ) {
                return NotFound();
            }

            return Ok( doctor );
        }

        [HttpPost]
        public IActionResult CreateDoctor( CreateDoctorRequest request )
        {
            var doctor = new Doctor
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Prescriptions = request.Prescriptions
            };

            _context.Attach( doctor );
            _context.Doctor.Add( doctor );
            _context.SaveChanges();

            return Ok( doctor );
        }

        [HttpPut( "{id}" )]
        public IActionResult UpdateDoctor( int id, UpdateDoctorRequest request )
        {
            var doctor = _context.Doctor.SingleOrDefault(d => d.IdDoctor==id);
            if ( doctor == null ) {
                return BadRequest( "Doctor nie istnieje" );
            }

            _context.Attach( doctor );
            if ( request.IdDoctor != null ) {
                doctor.IdDoctor = request.IdDoctor.Value;
                _context.Entry( doctor ).Property( "IdDoctor" ).IsModified = true;
            }
            if ( request.FirstName != null ) {
                doctor.FirstName = request.FirstName;
                _context.Entry( doctor ).Property( "FirstName" ).IsModified = true;
            }
            if ( request.LastName != null ) {
                doctor.FirstName = request.LastName;
                _context.Entry( doctor ).Property( "LastName" ).IsModified = true;
            }
            if ( request.Email != null ) {
                doctor.Email = request.Email;
                _context.Entry( doctor ).Property( "Email" ).IsModified = true;
            }
            if ( request.Prescriptions != null ) {
                doctor.Prescriptions = request.Prescriptions;
                _context.Entry( doctor ).Property( "Prescriptions" ).IsModified = true;
            }

            _context.SaveChanges();

            return Ok( "Aktualizacja ukończona" );
        }

        [HttpDelete( "{id}" )]
        public IActionResult DeleteDoctor( int id )
        {
            var doctor = _context.Doctor.SingleOrDefault(d => d.IdDoctor==id);
            if ( doctor == null ) {
                return BadRequest( "Doctor nie istnieje" );
            }
            _context.Doctor.Remove( doctor );
            _context.SaveChanges();

            return Ok( "Usuwanie ukończone" );
        }
    }
}
