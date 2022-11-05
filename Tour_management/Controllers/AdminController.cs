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
        public async Task<IActionResult> ApproveUser()
        {
            return View(await _dbCon.User.ToListAsync());
        }
        public async Task<IActionResult> ViewUsers()
        {
            var users = await _dbCon.User.ToListAsync();
            return View("ViewUsers",users);
        }
        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _dbCon.User == null)
            {
                return NotFound();
            }

            var user = await _dbCon.User
                .FirstOrDefaultAsync(m => m.user_id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _dbCon.User == null)
            {
                return NotFound();
            }

            var user = await _dbCon.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("user_id,username,password,Confirmpassword,email,mobile,dob,gender,address,city,state,country,status")] User user)
        {
            if (id != user.user_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbCon.Update(user);
                    await _dbCon.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.user_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ApproveUser));
            }
            return View(user);
        }
        // GET: Admins/Delete/5
        public async Task<IActionResult> Deletes(int? id)
        {
            if (id == null || _dbCon.User == null)
            {
                return NotFound();
            }

            var user = await _dbCon.User
                .FirstOrDefaultAsync(m => m.user_id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Deletes")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_dbCon.User == null)
            {
                return Problem("Entity set 'DbCon.User'  is null.");
            }
            var user = await _dbCon.User.FindAsync(id);
            if (user != null)
            {
                _dbCon.User.Remove(user);
            }

            await _dbCon.SaveChangesAsync();
            return RedirectToAction(nameof(ApproveUser));
        }

        private bool UserExists(int id)
        {
            return _dbCon.User.Any(e => e.user_id == id);
        }

        public async Task<IActionResult> packageSubmit(Package data)
        {
            var submit=await _dbCon.Package.AddAsync(data);
             await _dbCon.SaveChangesAsync();
            var select = await _dbCon.Package.ToListAsync(); 
            return View("Packages");

        }
        public async Task<IActionResult> Packages()
        {
            var packages = await _dbCon.Package.ToListAsync();
            return View("Packages",packages);
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
        public async Task<IActionResult> Bookings()
        {
            var bookings = await _dbCon.Booking.ToListAsync();
            return View("Bookings", bookings);
        }

    }
}
