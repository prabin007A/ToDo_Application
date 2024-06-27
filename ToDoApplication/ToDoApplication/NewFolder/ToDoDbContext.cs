using Microsoft.EntityFrameworkCore;
using ToDoApplication.Models;
namespace ToDoApplication.NewFolder
{
    public class ToDoDbContext:DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) 
        { 
        }
        public DbSet<ToDoInput>ToDoTable { get; set; }
    }
}
