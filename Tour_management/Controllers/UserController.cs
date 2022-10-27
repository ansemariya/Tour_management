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
        public IActionResult BookTrip()
        {
            return View();
        }
        public async Task<IActionResult> booking(Booking data)
        {
            var submit = await _dbCon.Booking.AddAsync(data);
            await _dbCon.SaveChangesAsync();
            return View("ViewPackages");

        }
    }
}
