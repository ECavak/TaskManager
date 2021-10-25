using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.DataAccess.Repository.Abstract;
using TaskManager.DataAccess.Repository.Concrete;
using TaskManager.Entities.Entities;
using TaskManager.WebUI.Models;

namespace TaskManager.WebUI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly ITaskRepository<Tasks> _taskRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserController( IRepository<User> userRepository, ITaskRepository<Tasks> taskRepository, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public async Task<IActionResult> Index(TaskVM taskVM)
        {

            var users = await _userManager.GetUserAsync(User);

            return View(taskVM);
        }
        [HttpGet]
        public async Task<IActionResult> ToDoList(User user)
        {
            var users =  await _userManager.GetUserAsync(User);
           

            var tasks = await _taskRepository.FilterByAsync(x=>x.userMail==users.Email);

           // var task = await _taskRepository.AsQueryableAsync();

            return View(tasks);
        }
        [HttpGet("/TaskDetail/:id")]
        public async Task<IActionResult> TaskDetail(string id)
        {
            var getId =await _taskRepository.GetByIdAsync(id);
            return View(getId);
        }

        [HttpPost]
        public async Task<IActionResult> CreateToDo(TaskVM entity,User user)
        {
            user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {

                var task = new Tasks
                {
                    TaskName = entity.task.TaskName,
                    TaskDescription = entity.task.TaskDescription,
                    User=user,
                    TaskSchedule = entity.task.TaskSchedule,
                    userMail=user.Email,
                    userID=user.Id.ToString()
                };

                //TODO: tarih hesaplama başka metoda ayrılacak
                if (task.TaskSchedule == Core.Enum.TaskSchedule.daily)
                {
                    task.EndDate = DateTime.Now.AddDays(1);
                }
                if (task.TaskSchedule == Core.Enum.TaskSchedule.weekly)
                {
                    task.EndDate = DateTime.Now.AddDays(7);
                }
                if(task.TaskSchedule == Core.Enum.TaskSchedule.monthly)
                {
                    task.EndDate = DateTime.Now.AddDays(30);
                }
                
                var result = await _taskRepository.AddAsync(task);

                user.Tasks = new List<Tasks> { task };

                if (result.Success)
                {
                    // await _userRepository.UpdateAsync(user.Tasks,user.Id.ToString());
                     
                    return RedirectToAction("Index", "User");

                }

            }
           
            return View("Index");
        }
    }
}
