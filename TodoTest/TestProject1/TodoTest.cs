using BackendAPI1.Controllers;
using BackendAPI1.data;
using BackendAPI1.models;
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
        public void Add_Todo_IsSaved_In_Db()
        {
            // Arrange
            var todo = new Todo { Id = 12345, Text = "Test", isDone = false };
            var service = new TodoService(_context);

            // Act
            var result = service.AddTodo(todo);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(todo.Text, result.Text);
            Assert.Equal(todo.isDone, result.isDone);

            // Assert that the todo is saved in the database
            _context.Todos.Single(x => x.Text == todo.Text);
        }

        [Fact]
        public void Add_Todo_Throws_Exception_When_Null()
        {
            // Arrange
            var service = new TodoService(_context);

            // Act
            var result = Assert.Throws<NullReferenceException>(() => service.AddTodo(null!));
        }
    }
}