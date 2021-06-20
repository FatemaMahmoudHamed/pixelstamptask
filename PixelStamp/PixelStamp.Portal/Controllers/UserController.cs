using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PixelStamp.Core.Dtos;
using PixelStamp.Core.Interfaces.Services;
using PixelStamp.Portal.Controllers;
using System;
using System.Threading.Tasks;

namespace ISolutionTask.Controllers
{
    public class UserController : BaseController
    {
    
        private readonly IUserService _userService;

        public UserController(IUserService userService)
            :base()
        {
           
            _userService = userService;
        }


        // GET: user/Details/5
        public async Task<IActionResult> Details()
        {
            PrepareViewBags();
            Guid id = new Guid(LoggedInUserId);
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetAsync(id);
            if (user.Data == null)
            {
                return NotFound();
            }

            return View(user.Data);
        }
        // GET: users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            PrepareViewBags();
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            return View(user.Data);
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserDto userDto)
        {
            PrepareViewBags();
            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.EditAsync(userDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var user = await _userService.GetAsync(userDto.Id.Value);
                    if (user.Data == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(User);
        }
        public void PrepareViewBags()
        {
            ViewBag.LoggedInUserId = LoggedInUserId;
            ViewBag.LoggedInUserName = LoggedInUserName;
            ViewBag.Role = Role;
        }

    }
}