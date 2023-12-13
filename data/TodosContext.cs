using Microsoft.EntityFrameworkCore;
using BackendAPI1.models;

namespace BackendAPI1.data
{
    public class TodosContext : DbContext
    {
        public TodosContext(DbContextOptions<TodosContext> options)
           : base(options)
        {
        }

        public TodosContext()
        {
        }
        public DbSet<Todo> Todos { get; set;} 
    }
}
