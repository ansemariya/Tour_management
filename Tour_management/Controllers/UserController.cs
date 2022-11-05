using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
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
        
        public async Task<IActionResult> BookTrip(int id)
       {
            int user_id = (int)HttpContext.Session.GetInt32("user_id");
            var package = await _dbCon.Package.Where(x=>x.pack_id==id).FirstOrDefaultAsync();

            var UserDetails = _dbCon.User.Where(x => x.user_id == user_id).FirstOrDefault();
            ViewBag.PackageDetails = _dbCon.Package.Where(x => x.pack_id == id).FirstOrDefault();
            BookTrip BukTrip=new BookTrip();
            BukTrip.pack_id= package.pack_id;
            BukTrip.nameOfPackage = package.nameOfPackage;
            BukTrip.price = package.price;
            BukTrip.duration = package.duration;
            BukTrip.persons = package.persons;
            BukTrip.email =UserDetails.email ;
            BukTrip.username = UserDetails.username;
            BukTrip.user_id = UserDetails.user_id;
            return View(BukTrip);
            

        }
        public async Task<IActionResult> BookingSubmit(Booking data)
        {
            var submit = await _dbCon.Booking.AddAsync(data);
            await _dbCon.SaveChangesAsync();
            return View("BookTrip");

        }
        // GET: Bookings
        public async Task<IActionResult> MyBookings()
        {
            int user_id = (int)HttpContext.Session.GetInt32("user_id");
            return View(await _dbCon.Booking.Where(x=> x.user_id ==user_id ).ToListAsync());

        }
        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _dbCon.Booking == null)
            {
                return NotFound();
            }

            var booking = await _dbCon.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("book_id,pack_id,booking_date,checkin,checkout,username")] Booking booking)
        {
            if (id != booking.book_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbCon.Update(booking);
                    await _dbCon.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.book_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MyBookings));
            }
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _dbCon.Booking == null)
            {
                return NotFound();
            }

            var booking = await _dbCon.Booking
                .FirstOrDefaultAsync(m => m.book_id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_dbCon.Booking == null)
            {
                return Problem("Entity set 'DbCon.Booking'  is null.");
            }
            var booking = await _dbCon.Booking.FindAsync(id);
            if (booking != null)
            {
                _dbCon.Booking.Remove(booking);
            }

            await _dbCon.SaveChangesAsync();
            return RedirectToAction(nameof(MyBookings));
        }

        private bool BookingExists(int id)
        {
            return _dbCon.Booking.Any(e => e.book_id == id);
        }
    }
}
