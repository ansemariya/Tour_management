using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tour_management.Data;
using Tour_management.Models;

namespace Tour_management.Controllers
{
    public class AdminController : Controller
    {
        private readonly DbCon _dbCon;
        public AdminController(DbCon dbCon)
        {
            _dbCon = dbCon;
        }
        public IActionResult CreatePackage()
        {
            return View();
        }
        

        public async Task<IActionResult> packageSubmit(Package data)
        {
            var submit=await _dbCon.Package.AddAsync(data);
             await _dbCon.SaveChangesAsync();
            var select = await _dbCon.Package.ToListAsync(); 
            return View("View");

        }
        public async Task<IActionResult> Packages()
        {
            var packages = await _dbCon.Package.ToListAsync();
            return View("packages",packages);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id > 0)
            {
                var packages = await _dbCon.Package.Where(x=>x.pack_id == id).FirstOrDefaultAsync();
                _dbCon.Package.Remove(packages);
                _dbCon.SaveChanges();
            }
           var list =  _dbCon.Package.ToList();
            return View("Packages", list);
        }
       
    }
}
