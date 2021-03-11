using CRUDUsingDapper.IServices;
using CRUDUsingDapper.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUDUsingDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _oStudentService;

        public StudentController(IStudentService oStudentService)
        {
            _oStudentService = oStudentService;
        }

        // GET: api/<StudentController>
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return _oStudentService.Gets();
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public Student Get(int id)
        {
            return _oStudentService.Get(id);
        }

        // POST api/<StudentController>
        [HttpPost]
        public Student Post([FromBody] Student value)
        {
            if (ModelState.IsValid) return _oStudentService.Save(value);
            return null;
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return _oStudentService.Delete(id);
        }
    }
}
