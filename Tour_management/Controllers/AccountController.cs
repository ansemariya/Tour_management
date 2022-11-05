using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using Tour_management.Data;
using Tour_management.Models;
using System.Net.Http;
namespace Tour_management.Controllers
{
    public class AccountController : Controller
    {
        private readonly DbCon _dbCon;
        private HttpClient _client;
        private readonly string baseurl = "https://localhost:7040/api/User";
        
        
        public AccountController(DbCon dbCon)
        {
            _dbCon = dbCon;
        }
        public IActionResult signup()
       {
             
            return View();
        }
        public IActionResult VerifiedMsg()
        {

            return View();
        }

        public IActionResult login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signup(User data)
        {
            
            if( _dbCon.User.Any(x=>x.username == data.username))
            {
                ViewBag.DuplicateMessage="Username already exist.";
                return View("Signup",data);
            }
            var submit = await _dbCon.User.AddAsync(data);
            
            await _dbCon.SaveChangesAsync();
            ModelState.Clear();
            TempData["username"]=data.username;
            TempData.Keep();



            return RedirectToAction("VerifiedMsg");

        }
        [HttpPost]
        public async Task<IActionResult> Login(User log)
        {
            var client = new HttpClient();
            string url = baseurl + "/Login";
            ////client.BaseAddress = new Uri(baseurl);
           StringContent content = new StringContent(JsonConvert.SerializeObject(log), Encoding.UTF8, "application/json");

            var request = await client.PostAsync(url,content);
            var response = await request.Content.ReadAsStringAsync();

            
            if(_dbCon.User.Any(x => x.username == log.username && x.password == log.password))
            {
                if(log.username=="Admin" && log.password == "Admin@123")
                {
                    HttpContext.Session.SetInt32("user_id", 2);
                    ViewBag.LoginMessage = "Successfully login.";
                    return RedirectToAction("Packages", "Admin");
                }
                else
                {
                    int user_id= await _dbCon.User.Where(x => x.username == log.username).Select(y=>y.user_id).FirstOrDefaultAsync();
                    
                        HttpContext.Session.SetInt32("user_id", user_id);
                    User use = await _dbCon.User.Where(x => x.username == log.username).FirstOrDefaultAsync();
                    if (use.status == 0)
                    {
                        
                        ViewBag.msg = "Your Account has not been Verified.";
                        return View("Login", user_id);
                    }
                    else if (use.status == 1)
                    {
                        
                        return RedirectToAction("ViewPackages", "User");
                    }
                    else 
                    {
                        ViewBag.msgerr = "Your Account has not been Verified.";
                        return View("Login");
                    }
   
                }         
            }
            else
            {
                ViewBag.ErrorMessage = "Wrong username & Password.";
                return View("Login", log);
            }
            
            
        }
    }
}
