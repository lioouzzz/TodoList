using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Data;
using ToDo.Filters;
using ToDo.Models;
using ToDo.ViewModels;


namespace ToDo.Controllers
{
    [CustomAuthorize]

    public class ToDoController : Controller
    {
        private readonly ToDoContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ToDoController(ToDoContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId=_userManager.GetUserId(User);
            var items = await _context.ToDos.Where(item => !item.IsDeleted && item.UserId == userId).ToListAsync();
            return View(items);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTodoItemViewModel model)
{
    if (ModelState.IsValid)
    {
        var todo = new TodoItem
        {
            Title = model.Title,
            Description = model.Description,
            // 從目前登入使用者取得 UserId
            UserId = _userManager.GetUserId(User),
            CreatedDate = DateTime.Now,
            IsDeleted = false,
            IsCompleted = false
        };

        _context.Add(todo);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    return View(model);
}

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var userId=_userManager.GetUserId(User);
            var todo = await _context.ToDos.FirstOrDefaultAsync(t=>t.Id==id && t.UserId ==userId);
            if (todo == null)
                return NotFound();

            var model = new EditTodoItemViewModel
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditTodoItemViewModel  model)
        {
            if (id != model.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var todo = await _context.ToDos.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
                if (todo == null)
                    return NotFound();

                // 更新資料
                todo.Title = model.Title;
                todo.Description = model.Description;
                todo.IsCompleted = model.IsCompleted;

                try
                {
                    _context.Update(todo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.ToDos.Any(t => t.Id == id && t.UserId == userId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = _userManager.GetUserId(User);

            var todo = await _context.ToDos.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (todo == null)
                return NotFound();
            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User);
            var todo = await _context.ToDos
                           .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId); 
            
            if (todo != null)
            {
                todo.IsDeleted = true;
                _context.Update(todo);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}