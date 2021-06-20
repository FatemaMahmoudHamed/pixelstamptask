using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using PixelStamp.Core.Interfaces.Services;
using PixelStamp.Core.Dtos;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System;

namespace PixelStamp.Portal.Controllers
{
    public class CourseController : BaseController
    {
        private readonly ICourseService _courseService;

        [ViewData]
        public string StatusMessage { get; set; }
        public CourseController(ICourseService courseService) 
            : base()
        {
            _courseService = courseService;
        }

        public async Task<IActionResult> Index(bool?allCourses)
        {
            PrepareViewBags();
            var res = await _courseService.GetAllAsync(allCourses.Value);
            return View(res.Data);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            PrepareViewBags();
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseService.GetAsync(id.Value);
            if (course.Data == null)
            {
                return NotFound();
            }

            return View(course.Data);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            PrepareViewBags();
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price")] CourseDto courseDto)
        {
            PrepareViewBags();
            if (ModelState.IsValid)
            {
                await _courseService.AddAsync(courseDto);

                return RedirectToAction("Index", "Course", new { allCourses = true });
            }
            return View(courseDto);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            PrepareViewBags();
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseService.GetAsync(id.Value);
            if (course == null)
            {
                return NotFound();
            }
            return View(course.Data);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price")] CourseDto courseDto)
        {
            PrepareViewBags();
            if (id != courseDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _courseService.EditAsync(courseDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var course = await _courseService.GetAsync(courseDto.Id);
                    if (course.Data== null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Course", new { allCourses = true });

            }
            return View(courseDto);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            PrepareViewBags();
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseService.GetAsync(id.Value);
            if (course.Data == null)
            {
                return NotFound();
            }

            return View(course.Data);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            PrepareViewBags();
            return RedirectToAction("Index", "Course", new { allCourses = true });

        }

        public async Task<IActionResult> Buy(int? id)
        {
            PrepareViewBags();
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseService.GetAsync(id.Value);
            if (course.Data == null)
            {
                return NotFound();
            }

            return View(course.Data);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Buy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyCourse(int id)
        {
            PrepareViewBags();
            var course = await _courseService.BuyCourseAsync(id,new Guid(LoggedInUserId));
            if (course.Data)
            {
                return RedirectToAction("Index", "Course", new { allCourses = true });
            }
            else 
            {
                return View("Error");
            }

        }

        public void PrepareViewBags()
        {
            ViewBag.LoggedInUserId =LoggedInUserId;
            ViewBag.LoggedInUserName = LoggedInUserName;
            ViewBag.Role = Role;
        }
    }
}
