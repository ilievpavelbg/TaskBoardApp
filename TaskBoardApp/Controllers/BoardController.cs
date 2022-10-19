using Microsoft.AspNetCore.Mvc;
using TaskBoardApp.Data;
using TaskBoardApp.Models;

namespace TaskBoardApp.Controllers
{
    public class BoardController : Controller
    {
        private readonly ApplicationDbContext context;

        public BoardController(ApplicationDbContext _context)
        {
            context = _context;
        }
    
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult All()
        {
            var boards = context.Boards
                .Select(b => new BoardViewModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Tasks = b.Tasks.Select(t => new TasksViewModel()
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        Owner = t.Owner.UserName
                    })
                }).ToList();

            return View(boards);
        }
    }
}
