using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectBlogCore.Data;
using ProjectBlogCore.Models.ViewModels;

namespace ProjectBlogCore.Controllers.Users
{
    public class UserRolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserRolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        //// GET: UserRoles
        //public async Task<IActionResult> Index()
        //{
        //    //var applicationDbContext = _context.ApplicationUserRoles.Include(p => p.UserId).Include(p => p.User);
        //    //return View(await applicationDbContext.ToListAsync());
        //    return View(await _context.ApplicationUserRoles.ToListAsync());

        //}       

        public IActionResult Index()
        {
            List<UserRolesViewModel> listaUserRoles = new List<UserRolesViewModel>();

            var UserRolesList = (from url in _context.UserRoles
                                 join use in _context.Users on url.UserId equals use.Id
                                 join rol in _context.Roles on url.RoleId equals rol.Id
                                 select new
                                 {
                                     use.UserName,
                                     rol.Name,
                                     url.UserId,
                                     url.RoleId
                                 }).ToList();

            foreach (var item in UserRolesList)
            {
                UserRolesViewModel Urlist = new UserRolesViewModel
                {                    
                    UserName = item.UserName,
                    Name = item.Name,
                    UserId = item.UserId,
                    RoleId = item.RoleId
                };

                listaUserRoles.Add(Urlist);
            }
            return View(listaUserRoles);
        }
               

        // GET: UserRoles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUserRole = await _context.ApplicationUserRoles
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (applicationUserRole == null)
            {
                return NotFound();
            }

            return View(applicationUserRole);
        }

        // GET: UserRoles/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }

        // POST: UserRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,RoleId")] ApplicationUserRole applicationUserRole)
        {
            if (ModelState.IsValid)
            {
                //applicationUserRole.UserId = Guid.NewGuid();
                _context.Add(applicationUserRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", applicationUserRole.UserId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "UserName", applicationUserRole.RoleId);
            return View(applicationUserRole);
        }

        // GET: UserRoles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUserRole = await _context.ApplicationUserRoles.FindAsync(id);
            if (applicationUserRole == null)
            {
                return NotFound();
            }
            return View(applicationUserRole);
        }
        

        // POST: UserRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ApplicationUserRole applicationUserRole)
        {
            if (id != applicationUserRole.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationUserRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserRoleExists(applicationUserRole.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(applicationUserRole);
        }

        // GET: UserRoles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUserRole = await _context.ApplicationUserRoles
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (applicationUserRole == null)
            {
                return NotFound();
            }

            return View(applicationUserRole);
        }

        // POST: UserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var applicationUserRole = await _context.ApplicationUserRoles.FindAsync(id);
            _context.ApplicationUserRoles.Remove(applicationUserRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationUserRoleExists(Guid id)
        {
            return _context.ApplicationUserRoles.Any(e => e.UserId == id);
        }

        //public async Task<ActionResult> Editar(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var applicationUserRole = await _context.ApplicationUserRoles.FindAsync(id);
        //    if (applicationUserRole == null)
        //    {
        //        return NotFound();
        //    }

        //    var userRolesViewModel = new UserRolesViewModel
        //    {
        //        UserId = applicationUserRole.UserId,
        //        RoleId = applicationUserRole.RoleId,
        //    };

        //    return View(applicationUserRole);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public async Task<IActionResult> Edit(UserRolesViewModel viewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var userId = _context.ApplicationUserRoles.FirstOrDefault(p => p.UserId == viewModel.UserId);
        //        var rolesId = _context.ApplicationUserRoles.FirstOrDefault(p => p.RoleId == viewModel.RoleId);

        //        if (userId == null && rolesId == null)
        //        {
        //            return NotFound();
        //        }

        //        userId.UserId = viewModel.UserId;
        //        rolesId.UserId = viewModel.RoleId;

        //        _context.Entry(userId).State = EntityState.Modified;
        //        _context.Entry(rolesId).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(viewModel);
        //}
    }
}
