using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers
{
    public class UserController : Controller
    {
        private static readonly List<User> _userList = new List<User>();

        // GET: User
        public ActionResult Index()
        {
            return View(_userList);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var user = _userList.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.Id = _userList.Any() ? _userList.Max(u => u.Id) + 1 : 1;
                    _userList.Add(user);
                    return RedirectToAction(nameof(Index));
                }
                return View(user);
            }
            catch
            {
                ModelState.AddModelError("", "An error occurred while creating the user.");
                return View(user);
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = _userList.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, User user)
        {
            try
            {
                if (id != user.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var existingUser = _userList.FirstOrDefault(u => u.Id == id);
                    if (existingUser == null)
                    {
                        return NotFound();
                    }

                    existingUser.Name = user.Name;
                    existingUser.Email = user.Email;
                    // existingUser.Phone = user.Phone;
                    // existingUser.Address = user.Address;

                    return RedirectToAction(nameof(Index));
                }
                return View(user);
            }
            catch
            {
                ModelState.AddModelError("", "An error occurred while updating the user.");
                return View(user);
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var user = _userList.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var user = _userList.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

                _userList.Remove(user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: User/Search
        public ActionResult Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return View("Index", _userList);
            }

            var searchResults = _userList.Where(u => 
                u.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                u.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                // u.Phone.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                // u.Address.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            return View("Index", searchResults);
        }
    }
}
