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
            ViewBag.SuccessMessage = "Registration Successful.";
            
            return View("signup");

        }
    }
}
