using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PixelStamp.Core.Interfaces.Services;
using PixelStamp.Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelStamp.Portal.Controllers
{
    public class ExamController : BaseController
    {
        private readonly IExamService _examService;
        private readonly IQuestionService _questionService;

        public ExamController(IExamService examService, IQuestionService questionService) 
            : base()
        {
            _examService = examService;
            _questionService = questionService;
        }

        // GET: exams
        public async Task<IActionResult> Index(int? courseId)
        {
            PrepareViewBags();
            TempData["CourseId"] = courseId;
            var result = await _examService.GetAllAsync(courseId.Value);
            return View(result.Data);
        }

        // GET: exams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Exam = await _examService.GetAsync(id.Value);
            if (Exam.Data == null)
            {
                return NotFound();
            }

            return View(Exam.Data);
        }

        // GET: exams/Create
        public IActionResult Create()
        {
            PrepareViewBags();
            var Exam = new Exam
            {
                CourseId = int.Parse(TempData["CourseId"].ToString())
            };
            TempData.Keep();
            GetQuestions();
            return View(Exam);
        }

        // POST: exams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExamDto Exam)
        {

            PrepareViewBags();
            if (ModelState.IsValid)
            {
                var questions = new List<Question>();
                var questionlist = await GetQuestions();
                foreach (var item in Exam.Questions)
                {
                    questions.Add(questionlist.Where(x=>x.Id==item).FirstOrDefault());
                }
                var res = await _examService.AddAsync(new Exam { CourseId = int.Parse(TempData["CourseId"].ToString()), Questions = questions });
                if (res.Data != null)
                {
                    TempData.Keep();
                    return RedirectToAction(nameof(Index), new { courseId = TempData["CourseId"] });
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            return View(Exam);
        }

        //GET: exams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            PrepareViewBags();
            if (id == null)
            {
                return NotFound();
            }

            var Exam = await _examService.GetAsync(id.Value);


            if (Exam.Data == null)
            {
                return NotFound();
            }
            await GetQuestions();
            return View(Exam.Data);
        }

        // POST: exams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] Exam Exam)
        {
            PrepareViewBags();
            if (id != Exam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _examService.EditAsync(Exam);

                }
                catch (DbUpdateConcurrencyException)
                {
                    var exam = await _examService.GetAsync(id);
                    if (Exam!= null)
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
            return View(Exam);
        }

        //// GET: exams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            PrepareViewBags();
            if (id == null)
            {
                return NotFound();
            }

            var Exam = await _examService.GetAsync(id.Value);


            if (Exam.Data == null)
            {
                return NotFound();
            }
            return View(Exam.Data);


        }

        // POST: exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            PrepareViewBags();
            TempData.Keep();
            await _examService.RemoveAsync(id);
            return RedirectToAction(nameof(Index), new { courseId = TempData["CourseId"] });
        }

        public async Task<IEnumerable<Question>> GetQuestions()
        {
            PrepareViewBags();
            var Questions = await _questionService.GetAllAsync();
            ViewBag.Questions = Questions.Data;
            return Questions.Data;
        }
        public void PrepareViewBags()
        {
            ViewBag.LoggedInUserId = LoggedInUserId;
            ViewBag.LoggedInUserName = LoggedInUserName;
            ViewBag.Role = Role;
        }
    }

}
