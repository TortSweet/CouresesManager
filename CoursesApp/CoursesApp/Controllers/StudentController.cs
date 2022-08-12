using System;
using CoursesApp.Models.Dtos;
using CoursesApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace CoursesApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ILogger _logger;

        public StudentController(IStudentService studentService/*, ILogger logger*/)
        {
            _studentService = studentService ?? throw new ArgumentNullException("Student service can't be null", nameof(studentService));
            //_logger = logger ?? throw new ArgumentNullException("Logger can't be null", nameof(logger));
        }

        [HttpPost("AddStudent")]
        public IActionResult AddStudent([FromForm] string name, string lastName, int groupId)
        {
            //_logger.LogInformation("Act method add student");
            if (groupId <= 0)
            {
                return NotFound();
            }
            var newGroup = _studentService.CreateStudent(name, lastName, groupId);

            if (newGroup == null)
            {
                return NotFound();
            }
            return RedirectToAction("ShowAllStudents");
        }
        [HttpGet]
        public ActionResult AddStudentView([FromQuery] int groupId)
        {
            //_logger.LogInformation("Act method add student view");

            if (groupId <= 0)
            {
                return NotFound();
            }
            TempData["groupId"] = groupId;
            return View("AddStudent");
        }
        public IActionResult DeleteStudent([FromQuery] int studentId)
        {
            //_logger.LogInformation("Act method delete student");

            if (studentId <= 0)
            {
                return NotFound();
            }

            _studentService.RemoveStudent(studentId);
            return RedirectToAction("ShowAllStudents");
        }

        [HttpPost("EditStudent")]
        public IActionResult EditStudent([FromForm] StudentDto studentDto)
        {
            //_logger.LogInformation("Act method edit student");

            if (studentDto == null)
            {
                return BadRequest();
            }
            var student = _studentService.UpdateStudent(studentDto.StudentId, studentDto.Name, studentDto.LastName);

            if (student == null)
            {
                return NotFound();
            }
            return RedirectToAction("ShowAllStudents");
        }

        [HttpGet("EditStudentView")]
        public ActionResult EditStudentView([FromQuery] int studentId)
        {
            //_logger.LogInformation("Act method edit student view");

            if (studentId <= 0)
            {
                return NotFound();
            }
            TempData["studentId"] = studentId;
            var student = _studentService.GetStudentList().FirstOrDefault(item => item.StudentId == studentId);

            return View("EditStudent", student);
        }

        [HttpGet("ShowStudentByGroupId")]
        public IActionResult ShowStudentByGroupId([FromQuery] int groupId)
        {
            //_logger.LogInformation("Act method show students by group id");

            if (groupId < 0)
            {
                return NotFound();
            }

            var students = _studentService.GetStudentByGroupId(groupId);
            return View("StudentView", students);
        }

        [HttpGet("ShowAllStudents")]
        public IActionResult ShowAllStudents()
        {
            //_logger.LogInformation("Act method show all students");

            var students = _studentService.GetStudentList();
            return View("StudentView", students);
        }
    }
}
