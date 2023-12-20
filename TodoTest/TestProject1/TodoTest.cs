using BackendAPI1.data;
using Microsoft.EntityFrameworkCore;

namespace TodoAPI.tests.Tests
{
    public class TodoTest
    {
        private readonly TodosContext _context;
        public TodoTest()
        {
            var options = new DbContextOptionsBuilder<TodosContext>().UseInMemoryDatabase("InMemory").Options;
            _context = new TodosContext(options);
        }

        [Fact]
        public void Test1()
        {

        }
    }
}