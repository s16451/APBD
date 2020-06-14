using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeFirst.Models;
using Microsoft.AspNetCore.Http;
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
    }
}
