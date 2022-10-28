using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tour_management.Data;
using Tour_management.Models;
namespace Tour_management.Controllers
{
    
    public class UserController : Controller
    {
        private readonly DbCon _dbCon;
        public UserController(DbCon dbCon)
        {
            _dbCon = dbCon;
        }
       
        public async Task<IActionResult> ViewPackages()
        {
            var packages = await _dbCon.Package.ToListAsync();
            return View("ViewPackages", packages);
        }
        
        public async Task<IActionResult> BookTrip(int pack_id)
        {
            int user_id = (int)HttpContext.Session.GetInt32("user_id");
            var package = await _dbCon.Package.Where(x=>x.pack_id==pack_id).FirstOrDefaultAsync();
            ViewBag.UserDetails = _dbCon.User.Where(x => x.user_id == user_id).FirstOrDefault();
            return View(package);
            

        }
    }
}
