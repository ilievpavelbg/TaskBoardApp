using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskBoardApp.Data;
using TaskBoardApp.Data.Entities;
using TaskBoardApp.Models;
using Task = TaskBoardApp.Data.Entities.Task;

namespace TaskBoardApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext context;

        public TasksController(ApplicationDbContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var task = new TaskFormModel()
            {
                Boards = context.Boards.Select(b => new TaskBoarModel()
                {
                    Id = b.Id,
                    Name = b.Name
                })
            };

            return View(task);
        }

        [HttpPost]
        public IActionResult Create(TaskFormModel model)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var task = new Task()
            {
                Title = model.Title,
                Description = model.Description,
                CreatedOn = DateTime.Now,
                BoardId = model.BoardId,
                OwnerId = user
            };

            context.Tasks.Add(task);
            context.SaveChanges();

            return RedirectToAction("All", "Board");
        }

        public IActionResult Details(int Id)
        {
            var task = context.Tasks
                .Where(t => t.Id == Id)
                .Select(t => new TaskDetailsViewModel()
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreatedOn = t.CreatedOn.ToString("dd/MM/YYYY"),
                    Board = t.Board.Name,
                    Owner = t.Owner.UserName
                })
                .FirstOrDefault();

            if (task == null)
            {
                return BadRequest();
            }

            return View(task);
        }

        public IActionResult Edit(int Id)
        {
            var task = context.Tasks
                .Where(t => t.Id == Id)
                .Select(t => new TaskFormModel()
                {
                    Title = t.Title,
                    Description = t.Description,
                    BoardId = t.BoardId,
                    Boards = context.Boards.Select(b => new TaskBoarModel()
                    {
                        Id = b.Id,
                        Name = b.Name
                    }).ToList()
                })
                .FirstOrDefault();

            if (task == null)
            {
                return BadRequest();
            }

            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(int Id, TaskFormModel model)
        {

            var task = context.Tasks.Find(Id);

            if (task == null)
            {
                return BadRequest();
            }
            
            task.Title = model.Title;
            task.Description = model.Description;
            task.BoardId = model.BoardId;
            
            context.Tasks.Update(task);
            context.SaveChanges();

            return RedirectToAction("All", "Board");
        }



        public IActionResult Delete(int? id)
        {
            var task = context.Tasks.Find(id);

            var taskModel = new TasksViewModel()
            {
                Description = task.Description,
                Title = task.Title
            };

            return View(taskModel);
        }

        // POST: /Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(TasksViewModel model)
        {
            var task = context.Tasks.Find(model.Id);
           
            context.Tasks.Remove(task);
            context.SaveChanges();
            return RedirectToAction("All", "Board");
        }
    }
}
