using System;
using CoursesApp.Models.Dtos;
using CoursesApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace CoursesApp.Controllers
{
    public class GroupController : Controller
    {
        private readonly IStudGroupService _studGroupService;
        private readonly ILogger _logger;

        public GroupController(IStudGroupService studGroupService/*, ILogger logger*/)
        {
            _studGroupService = studGroupService ?? throw new ArgumentNullException("Course service can't be null", nameof(studGroupService));
            //_logger = logger ?? throw new ArgumentNullException("Logger can't be null", nameof(logger));
        }

        [HttpGet("ShowAllGroup")]
        public IActionResult ShowAllGroup()
        {
            //_logger.LogInformation("Act method show all group");

            var groups = _studGroupService.GetStudGroupList();
            return View("GroupView", groups);
        }

        [HttpGet("ShowGroupById")]
        public IActionResult ShowGroupById(int courseId)
        {
            //_logger.LogInformation("Act method show group by id");

            if (courseId < 0)
            {
                return NotFound();
            }

            var groups = _studGroupService.GetStudGroupByCourseId(courseId);
            return View("GroupView", groups);
        }

        [HttpPost("AddGroup")]
        public IActionResult AddGroup([FromForm] string name, int courseId)
        {
            //_logger.LogInformation("Act method add group");

            if (courseId <= 0)
            {
                return NotFound();
            }
            
            var newGroup = _studGroupService.CreateStudGroup(name, courseId);
            
            return RedirectToAction("ShowAllGroup");
        }
        
        public ActionResult AddGroupView([FromQuery] int courseId)
        {
            //_logger.LogInformation("Act method add group view");

            if (courseId <= 0)
            {
                return NotFound();
            }
            TempData["courseId"] = courseId;
            return View("AddGroup");
        }
        public IActionResult DeleteGroup([FromQuery] int groupId)
        {
            //_logger.LogInformation("Act method delete group");

            if (groupId <= 0)
            {
                return NotFound();
            }
            _studGroupService.RemoveStudGroup(groupId);
            return RedirectToAction("ShowAllGroup");
        }

        [HttpPost("EditGroup")]
        public IActionResult EditGroup([FromForm] StudGroupDto studGroupDto)
        {
            //_logger.LogInformation("Act method edit group");

            if (studGroupDto == null)
            {
                return BadRequest();
            }
            var studGroup = _studGroupService.UpdateStudGroup(studGroupDto.GroupId, studGroupDto.Name);

            if (studGroup == null)
            {
                return NotFound();
            }
            return RedirectToAction("ShowAllGroup");
        }

        [HttpGet("EditGroupView")]
        public ActionResult EditGroupView([FromQuery] int groupId)
        {
            //_logger.LogInformation("Act method edit group view");

            if (groupId <= 0)
            {
                return NotFound();
            }
            TempData["groupId"] = groupId;
            var group = _studGroupService.GetStudGroupList().FirstOrDefault(item => item.GroupId == groupId);
            return View("EditGroup", group);
        }
    }
}
