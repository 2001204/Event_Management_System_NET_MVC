using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationIdentity.DTO;
using WebApplicationIdentity.Models;

namespace WebApplicationIdentity.Controllers
{

   // [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;

        }

        
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            IList<RoleStore> roleStores = new List<RoleStore>();
            foreach(var rol in roles)
            {
                RoleStore roleStore = new RoleStore();
                roleStore.id = rol.Id;
                roleStore.RoleName = rol.Name;
                roleStores.Add(roleStore);
            }

            return View(roleStores);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpGet]
        public IActionResult GetUsers(string searchString)
        {
            var users = _userManager.Users;

            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.FirstName.Contains(searchString) || u.LastName.Contains(searchString));
            }

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleStore roleStore)
        {
            var alreadyAdded=await _roleManager.RoleExistsAsync(roleStore.RoleName);
            if(!alreadyAdded)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleStore.RoleName));
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id == null || _userManager.Users == null)
            {
                return NotFound();
            }

            var roleEntity = await _userManager.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roleEntity == null)
            {
                return NotFound();
            }

            return View(roleEntity);
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmed(string id)
        {
            if (_userManager.Users == null)
            {
                return Problem("Entity set 'AppDbContext.Roles' is null.");
            }

            var roleEntity = await _userManager.FindByIdAsync(id);
            if (roleEntity != null)
            {
                var result = await _userManager.DeleteAsync(roleEntity);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(GetUsers));
                }
                else
                {
                    // Handle the error, e.g., return a view with error messages
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            var roleStore = new RoleStore
            {
                id = role.Id,
                RoleName = role.Name
            };

            return View(roleStore);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            var roleStore = new RoleStore
            {
                id = role.Id,
                RoleName = role.Name
            };

            return View(roleStore);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, RoleStore roleStore)
        {
            if (id != roleStore.id)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            role.Name = roleStore.RoleName;

            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return View(roleStore);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            var roleStore = new RoleStore
            {
                id = role.Id,
                RoleName = role.Name
            };

            return View(roleStore);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return NotFound();
        }
    }
}
