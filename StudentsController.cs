using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi1.Models;

namespace WebApi1.Controllers
{
    public class StudentsController : ApiController
    {
        Student[] students = new Student[]
        {
            new Student {Id=1, Grade=1,FullName="Mohammad Ali" },
            new Student {Id=2, Grade=1,FullName="Omar Habib" },
            new Student {Id=3, Grade=1,FullName="Shadi Jamil" },
            new Student {Id=4, Grade=2,FullName="Kamel bbadri" },
            new Student {Id=5, Grade=2,FullName="Khali Ahmad" }
        };

        public IEnumerable<Student> GetAllStudents()
        {
            return students;
        }

        public IHttpActionResult GetStudent(int id)
        {
            var student = students.FirstOrDefault(std => std.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
    }
}
