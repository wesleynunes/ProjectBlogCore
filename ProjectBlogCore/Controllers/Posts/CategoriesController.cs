using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectBlogCore.Data;
using ProjectBlogCore.Models.Post;
using ProjectBlogCore.Services;

namespace ProjectBlogCore.Controllers.Posts
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public CategoriesController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Categories.Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            // pegar usuario logado id ou username 
            var UserLogin = await _userManager.GetUserAsync(User);

            // Verifica se a categoria existe 
            Category CategoriesExists = await _context.Categories.SingleOrDefaultAsync(m => m.Name == category.Name);

            // Mensagem se category ja existe
            if (CategoriesExists != null)
            {              
                ModelState.AddModelError(string.Empty, "Esta Categoria já esta Cadastrada!!");                
            }

            if (ModelState.IsValid)
            {
                category.CategoryId = Guid.NewGuid();
                category.CreateTime = DateTime.Now;
                category.UpdateTime = DateTime.Now;
                category.UserId = UserLogin.Id;          
                _context.Add(category);
                await _context.SaveChangesAsync();                
                TempData["MessagePanelCategory"] = "Categoria Salva com sucesso";
                return RedirectToAction(nameof(Index));
            }
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", category.UserId);
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", category.UserId);
            return View(category);
        }

             

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Category category)
        {

            // pegar usuario logado id ou username 
            var Userlogin = await _userManager.GetUserAsync(User);
            
            //Verifica o Id da categoria
            Category categories = _context.Categories.Find(category.CategoryId);

            if (ModelState.IsValid)
            {
                if (!CheckCategory(category.Name, category.CategoryId))
                {
                    categories.Name = category.Name;
                    categories.UpdateTime = DateTime.Now;
                    categories.UserId = Userlogin.Id;
                    _context.Update(categories);
                    await _context.SaveChangesAsync();
                    TempData["MessagePanelCategory"] = "Categoria atualizada com sucesso";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Name", "Esta categoria já está em uso");
                    return View(category);
                }
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            TempData["MessagePanelCategory"] = "Categoria Deletada com sucesso";
            return RedirectToAction(nameof(Index));
        }

        public bool CheckCategory(string name, Guid id)
        {
            var DoesExistcategory = (from u in _context.Categories
                                     where u.Name == name
                                     where u.CategoryId != id
                                     select u).FirstOrDefault();
            if (DoesExistcategory != null)
                return true;
            else
                return false;
        }


        private bool CategoryExists(Guid id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
