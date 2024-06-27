using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using ToDoApplication.Models;
using ToDoApplication.NewFolder;

namespace ToDoApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ToDoDbContext dbContext;

        public HomeController(ToDoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var ToDoList=dbContext.ToDoTable.ToList();
            return View(ToDoList);
        }

        [HttpPost]
        public IActionResult Index(string userInput)
        {
            if (!string.IsNullOrEmpty(userInput))
            {
                var inp = new ToDoInput
                {
                    Task = userInput,
                };

                dbContext.ToDoTable.Add(inp);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Msg = "Please enter something";
                return View();
            }
        }

        [HttpPost]
        public IActionResult ClearTasks()
        {
            dbContext.ToDoTable.RemoveRange(dbContext.ToDoTable);
            dbContext.SaveChanges();
            dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('ToDoTable', RESEED, 0)");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateStatus(int id)
        {
            var todo = dbContext.ToDoTable.FirstOrDefault(x => x.Id == id);
            if (todo != null)
            {
                todo.Status = "Completed";
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {

            var todo = dbContext.ToDoTable.FirstOrDefault(x => x.Id == id);
            if (todo != null)
            {
                dbContext.ToDoTable.Remove(todo);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
