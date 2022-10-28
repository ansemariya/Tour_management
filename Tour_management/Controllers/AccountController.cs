using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Tour_management.Data;
using Tour_management.Models;
namespace Tour_management.Controllers
{
    public class AccountController : Controller
    {
        private readonly DbCon _dbCon;
        public AccountController(DbCon dbCon)
        {
            _dbCon = dbCon;
        }
        public IActionResult signup()
       {
             
            return View();
        }
        public IActionResult login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> signup(User data)
        {
            if( _dbCon.User.Any(x=>x.username == data.username))
            {
                ViewBag.DuplicateMessage="Username already exist.";
                return View("signup",data);
            }
            var submit = await _dbCon.User.AddAsync(data);
            await _dbCon.SaveChangesAsync();
            ModelState.Clear();
            

            return RedirectToAction("login", "Account");

        }
        [HttpPost]
        public async Task<IActionResult> login(User log)
        {
            
            if(_dbCon.User.Any(x => log.username == log.username && x.password == log.password))
            {
                if(log.username=="Admin" && log.password == "Admin@123")
                {
                    HttpContext.Session.SetInt32("user_id", 2);
                    ViewBag.LoginMessage = "Successfully login.";
                    return RedirectToAction("Packages", "Admin");
                }
                else
                {
                    int user_id=_dbCon.User.Where(x => x.username == log.username).Select(y=>y.user_id).FirstOrDefault();
                    HttpContext.Session.SetInt32("user_id",user_id);
                    ViewBag.LoginMessage = "Successfully login.";
                    return RedirectToAction("ViewPackages", "User");
                }         
            }
            else
            {
                ViewBag.ErrorMessage = "Wrong username & Password.";
                return View("login", log);
            }
            
            
        }
    }
}
