using DMAWebProject.DAL;
using DMAWebProject.Models;
using DMAWebProject.ViewModels;
using FastenyApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DMAWebProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext appDbContext;
        private readonly UserManager<ProgramUser>  _userManager;
        private readonly SignInManager<ProgramUser> _signInManager;
        private readonly IEmailService   _emailService;
        public AccountController(AppDbContext _appDbContext, UserManager<ProgramUser> userManager, IEmailService emailService, SignInManager<ProgramUser> signInManager)
        {
            appDbContext = _appDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ProgramUser programUser = new ProgramUser
            {
                Email = model.Email,
                 UserName= model.Email
            };

            var result = await _userManager.CreateAsync(programUser, model.Password);
            if (result.Succeeded)
            {
                RedirectToAction("Index", "Home");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something incorrent");
            }

           var user= await _userManager.FindByEmailAsync(model.Email);
            if (user!=null)
            {
               var result= await _signInManager.PasswordSignInAsync(user,model.Password,model.IsRemember,false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Password or email incorrent");
                }
            }
            //_emailService.SendEmailAsync()
           return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
