using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.DataAccess.Repository.Abstract;
using TaskManager.Entities.Entities;

using TaskManager.WebUI.Models;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Tasks> _taskRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public HomeController(SignInManager<User> signInManager, IRepository<User> userRepository, IRepository<Tasks> taskRepository,UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
           
            return View();
        }
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password,false,false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "User");
                }
                
            }
            return View(loginVM);

        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserRegisterVM model)
        {
            if (ModelState.IsValid)
            {
               
                    var user = new User
                    {
                        UserName = model.Email,
                        Email=model.Email,
                        Tasks=null
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user,false);
                    return RedirectToAction("Index", "User");

                }
               
            }
         
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home" );
        }
     
    }
}
