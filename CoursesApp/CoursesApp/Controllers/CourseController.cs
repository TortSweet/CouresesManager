using System;
using CoursesApp.Models.Dtos;
using CoursesApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace CoursesApp.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ILogger _logger;

        public CourseController(ICourseService courseService/*, ILogger logger*/)
        {
            _courseService = courseService ?? throw new ArgumentNullException("Course service can't be null", nameof(courseService));
            //_logger = logger ?? throw new ArgumentNullException("Logger can't be null", nameof(logger));
        }

        public IActionResult ShowCourses()
        {
            //_logger.LogInformation("Act method show all course");

            var courses = _courseService.GetCoursesList();

            return View("CourseView", courses.AsEnumerable());
        }

        public IActionResult DeleteCourse([FromQuery] int courseId)
        {
            //_logger.LogInformation("Act method delete course");

            if (courseId <= 0)
            {
                return NotFound();
            }
            _courseService.RemoveCourse(courseId);

            return RedirectToAction("ShowCourses");
        }

        [HttpPost("AddCourse")]
        public IActionResult AddCourse([FromForm] string name, string description)
        {
            //_logger.LogInformation("Act method add course");

            if (name == null || description == null)
            {
                return NotFound();
            }
            var course = _courseService.CreateCourse(name, description);
            
            return RedirectToAction("ShowCourses");
        }
        public ActionResult AddCourseView()
        {
            //_logger.LogInformation("Act method add course view");

            return View("AddCourse");
        }

        [HttpPost("EditCourse")]
        [ValidateAntiForgeryToken]
        public IActionResult EditCourse([FromForm]CourseDto courseDto)
        {
            //_logger.LogInformation("Act method edit course");

            if (courseDto == null)
            {
                return BadRequest();
            }

            var course = _courseService.UpdateCourse(courseDto.CourseId, courseDto.Name, courseDto.Description);

            if (course == null)
            {
                return NotFound();
            }
            return RedirectToAction("ShowCourses");
        }

        [HttpGet("EditCourseView")]
        public ActionResult EditCourseView([FromQuery] int courseId)
        {
            //_logger.LogInformation("Act method edit course view");

            if (courseId <= 0)
            {
                return NotFound();
            }
            TempData["courseId"] = courseId;
            var course = _courseService.GetCoursesList().FirstOrDefault(item => item.CourseId == courseId);
            return View("EditCourse", course);
        }
    }
}
