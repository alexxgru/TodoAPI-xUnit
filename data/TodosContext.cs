using BackendAPI1.models;
using Microsoft.EntityFrameworkCore;

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
