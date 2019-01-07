using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectBlogCore.Data;
using ProjectBlogCore.Models.Post;
using X.PagedList;

namespace ProjectBlogCore.Controllers.Posts
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _hostingEnv;

        public PostsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnv = hostingEnv;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Posts.Include(p => p.Categories).Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Posts
        public async Task<IActionResult> List()
        {
            var list = _context.Posts.Include(p => p.Categories).Include(p => p.User);
            return View(await list.ToListAsync());
        }
            
        
        //public IActionResult List(string busca = "", int pagina = 1)
        //{
        //    var posts = _context.Posts.Where(p => p.Title.Contains(busca))
        //                                    .OrderByDescending(p => p.PostId)
        //                                    .ToPagedList(pagina, 3);

        //    ViewBag.Busca = busca;

        //    return View(posts);
        //}


        // GET: Posts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Categories)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {            
            // Verifica se o post ja existe 
            Post PostsExists = await _context.Posts.SingleOrDefaultAsync(m => m.Title == post.Title);

            // Mensagem se category ja existe
            if (PostsExists != null)
            {
                ModelState.AddModelError(string.Empty, "Este titulo ja esta cadastrado!!");
            }

            // pegar usuario logado id ou username 
            var UserLogin = await _userManager.GetUserAsync(User);

            // variavel com caminho das pastas
            string uploadPath = "uploads/img/";

            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                post.PostId     = Guid.NewGuid();
                post.CreateTime = DateTime.Now;
                post.UpdateTime = DateTime.Now;
                post.CategoryId = post.CategoryId;
                post.UserId     = UserLogin.Id;

                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        //var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);  // gera um Guid como nome da imagem
                        var fileName = post.PostId + Path.GetExtension(file.FileName);  // o nome da imagem e o mesmo do Name
                        var uploadPathWithfileName = Path.Combine(uploadPath, fileName);

                        var uploadAbsolutePath = Path.Combine(_hostingEnv.WebRootPath, uploadPathWithfileName);

                        using (var fileStream = new FileStream(uploadAbsolutePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            post.ImageFile = uploadPathWithfileName;
                        }
                    }
                }
                _context.Add(post);
                await _context.SaveChangesAsync();
                TempData["MessagePanelPost"] = "Post salvo com sucesso";
                return RedirectToAction(nameof(List));
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", post.CategoryId);
            return View(post);



            //// pegar usuario logado id ou username 
            //var UserLogin = await _userManager.GetUserAsync(User);

            //// Verifica se o post ja existe 
            //Post PostsExists = await _context.Posts.SingleOrDefaultAsync(m => m.Title == post.Title);

            //// Mensagem se category ja existe
            //if (PostsExists != null)
            //{
            //    ModelState.AddModelError(string.Empty, "Este titulo ja esta cadastrado!!");
            //}

            //if (ModelState.IsValid)
            //{
            //    post.PostId = Guid.NewGuid();
            //    post.CreateTime = DateTime.Now;
            //    post.UpdateTime = DateTime.Now;
            //    post.CategoryId = post.CategoryId;
            //    post.UserId = UserLogin.Id;
            //    _context.Add(post);
            //    await _context.SaveChangesAsync();
            //    TempData["MessagePanelPost"] = "Post salvo com sucesso";
            //    return RedirectToAction(nameof(Index));
            //}

            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", post.CategoryId);
            ////ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            //return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", post.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Post post)
        {                       
            //Verifica o Id da categoria
            Post posts = _context.Posts.Find(post.PostId);

            //pegar usuario logado id ou username
            var Userlogin = await _userManager.GetUserAsync(User);

            // variavel que armazena o caminho para imagens serem salvas
            string uploadPath = "uploads/img/";
            
            // Contexto para files
            var files = HttpContext.Request.Form.Files;

            if (ModelState.IsValid)
            {
                posts.Title = post.Title;
                posts.Content = post.Content;
                posts.UpdateTime = DateTime.Now;
                posts.Tag = post.Tag;
                posts.CategoryId = post.CategoryId;
                posts.UserId = Userlogin.Id;
                posts.ImageFile = post.ImageFile;

                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        //var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);  // gera um Guid como nome da imagem
                        var fileName = post.PostId + Path.GetExtension(file.FileName);  // o nome da imagem e o mesmo do Name
                        var uploadPathWithfileName = Path.Combine(uploadPath, fileName);

                        var uploadAbsolutePath = Path.Combine(_hostingEnv.WebRootPath, uploadPathWithfileName);

                        using (var fileStream = new FileStream(uploadAbsolutePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            posts.ImageFile = uploadPathWithfileName;
                        }
                    }
                }

                if (!CheckPost(post.Title, post.PostId))
                {
                    _context.Update(posts);
                    await _context.SaveChangesAsync();
                    TempData["MessagePanelPost"] = "Post atualizado com sucesso";
                    return RedirectToAction(nameof(List));
                }
                else
                {
                    ModelState.AddModelError("Title", "Este Titulo já está em uso");
                }
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", post.CategoryId);
            return View(post);                                                                      
            

            //if (id != post.PostId)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(post);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!PostExists(post.PostId))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", post.CategoryId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            //return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Categories)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            TempData["MessagePanelPost"] = "Post deletado com sucesso";
            return RedirectToAction(nameof(List));
        }

        public bool CheckPost(string title, Guid id)
        {
            var DoesExistpost = (from u in _context.Posts
                                     where u.Title == title
                                     where u.PostId != id
                                     select u).FirstOrDefault();
            if (DoesExistpost != null)
                return true;
            else
                return false;
        }


        private bool PostExists(Guid id)
        {
            return _context.Posts.Any(e => e.PostId == id);
        }
    }
}
