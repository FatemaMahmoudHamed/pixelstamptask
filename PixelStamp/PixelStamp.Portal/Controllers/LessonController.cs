using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using PixelStamp.Portal.Controllers;
using PixelStamp.Core.Dtos;
using PixelStamp.Portal.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using PixelStamp.Core.Interfaces.Services;

namespace PixelStamp.Controllers
{
    public class LessonController : BaseController
    {
        private readonly ILessonService _lessonService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public LessonController(ILessonService lessonService,IHostingEnvironment hostingEnvironment) :base()
        {
            _lessonService = lessonService;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: lessons
        public async Task<IActionResult> Index(int? courseId)
        {
            PrepareViewBags();
            TempData["CourseId"] = courseId;
            var result = await _lessonService.GetAllAsync(courseId.Value);
            return View(result.Data);
        }

        // GET: lessons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            PrepareViewBags();
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _lessonService.GetAsync(id.Value);
            if (lesson.Data == null)
            {
                return NotFound();
            }

            return View(lesson.Data);
        }

        // GET: lessons/Create
        public IActionResult Create()
        {
            PrepareViewBags();
            var lessonDto = new LessonViewModel
            {
                CourseId = int.Parse(TempData["CourseId"].ToString())
            };
            TempData.Keep();
            return View(lessonDto);
        }

        // POST: lessons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LessonViewModel lessonDto)
        {
           
            PrepareViewBags();
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (lessonDto.Video != null) 
                {
                    var UploadesFiles=Path.Combine(_hostingEnvironment.WebRootPath, "Videos");
                    uniqueFileName=Guid.NewGuid().ToString() + "_" + lessonDto.Video.FileName;
                    string FilePath=Path.Combine(UploadesFiles, uniqueFileName);
                    lessonDto.Video.CopyTo(new FileStream(FilePath, FileMode.Create));

                }
                
                var res= await _lessonService.AddAsync(new LessonDto { CourseId= int.Parse(TempData["CourseId"].ToString()),Description=lessonDto.Description,Video=uniqueFileName });
                if(res.Data!=null)
                {
                    TempData.Keep();
                    return RedirectToAction(nameof(Index), new { courseId = TempData["CourseId"]});
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            return View(lessonDto);
        }

        //GET: lessons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            PrepareViewBags();
            if (id == null)
            {
                return NotFound();
            }

            var lesson =await  _lessonService.GetAsync(id.Value);

          
            if (lesson.Data == null)
            {
                return NotFound();
            }
            return View(lesson.Data);
        }

        // POST: lessons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] LessonDto lessonDto)
        {
            PrepareViewBags();
            if (id != lessonDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _lessonService.EditAsync(lessonDto);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    var lesson = await _lessonService.GetAsync(id);
                    if (lesson.Data !=null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData.Keep();
                return RedirectToAction(nameof(Index), new { courseId = TempData["CourseId"] });
            }
            return View(lessonDto);
        }

        //// GET: lessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
         
            PrepareViewBags();
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _lessonService.GetAsync(id.Value);


            if (lesson.Data == null)
            {
                return NotFound();
            }
            return View(lesson.Data);


        }

        // POST: lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            PrepareViewBags();
            TempData.Keep();
            await  _lessonService.RemoveAsync(id);
            return RedirectToAction(nameof(Index), new { courseId = TempData["CourseId"] });
        }
        public void PrepareViewBags()
        {
            ViewBag.LoggedInUserId = LoggedInUserId;
            ViewBag.LoggedInUserName = LoggedInUserName;
            ViewBag.Role = Role;
        }
    }
}
