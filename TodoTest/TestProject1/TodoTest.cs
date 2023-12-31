using BackendAPI1.Controllers;
using BackendAPI1.data;
using BackendAPI1.models;
using Microsoft.EntityFrameworkCore;

namespace TodoAPI.tests.Tests
{
    public class TodoTest
    {
        private TodosContext _context;
        public TodoTest()
        {
            var options = new DbContextOptionsBuilder<TodosContext>().UseInMemoryDatabase("InMemory").Options;
            _context = new TodosContext(options);
        }

        // Reset the database before each test
        private Task ResetContext()
        {
            _context.Todos.RemoveRange(_context.Todos);
            _context.SaveChangesAsync();

            return Task.CompletedTask;
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

        [Fact]
        public async void List_All_Todos()
        {
            // Arrange
            await ResetContext();
            var todo1 = new Todo { Id = 12345, Text = "Test1", isDone = false };
            var todo2 = new Todo { Id = 12345, Text = "Test2", isDone = false };
            var service = new TodoService(_context);
            service.AddTodo(todo1);
            service.AddTodo(todo2);

            // Act
            var result = service.GetNotes(null);

            // Assert
            Assert.Equal(2, result.Length);
            Assert.Contains(result, x => x.Text == todo1.Text);
            Assert.Contains(result, x => x.Text == todo1.Text);
        }

        [Fact]
        public async void List_Completed_Todos()
        {
            // Arrange
            await ResetContext();
            var todo1 = new Todo { Id = 12345, Text = "Test1", isDone = true };
            var todo2 = new Todo { Id = 12345, Text = "Test2", isDone = false };
            var service = new TodoService(_context);
            service.AddTodo(todo1);
            service.AddTodo(todo2);

            // Act
            var result = service.GetNotes(true);

            // Assert
            Assert.Single(result);
            Assert.Equal(todo1.Text, result[0].Text);
        }

        [Fact]
        public async void List_InComplete_Todos()
        {
            // Arrange
            await ResetContext();
            var todo1 = new Todo { Id = 12345, Text = "Test1", isDone = true };
            var todo2 = new Todo { Id = 12345, Text = "Test2", isDone = false };
            var service = new TodoService(_context);
            service.AddTodo(todo1);
            service.AddTodo(todo2);

            // Act
            var result = service.GetNotes(false);

            // Assert
            Assert.Single(result);
            Assert.Equal(todo2.Text, result[0].Text);
        }

        [Fact]
        public async void Delete_Todo_Returns_Details()
        {
            // Arrange
            await ResetContext();
            var todo = new Todo { Id = 12345, Text = "Test1", isDone = true };
            var service = new TodoService(_context);
            var addedTodo = service.AddTodo(todo);

            // Act
            var result = service.DeleteNote(addedTodo.Id);

            // Assert
            Assert.Equal(result.Text, todo.Text);

            // Assert that the todo is deleted from the database
            Assert.Empty(_context.Todos);
        }

        [Fact]
        public async void Delete_Todo_Invalid_Id_Throws_Exception()
        {
            // Arrange
            await ResetContext();
            var todo = new Todo { Id = 12345, Text = "Test1", isDone = true };
            var service = new TodoService(_context);
            service.AddTodo(todo);

            // Act
            //Id doesn't exist in db
            var result = Assert.Throws<Exception>(() => service.DeleteNote(1512512));
        }

        [Fact]
        public async void Update_Note_Status_To_InComplete_Returns_Details()
        {
            // Arrange
            await ResetContext();
            var todo = new Todo { Id = 12345, Text = "Test1", isDone = true };
            var service = new TodoService(_context);
            var addedTodo = service.AddTodo(todo);

            // Act
            var result = service.ChangeNote(new Todo { Id = addedTodo.Id, Text = "Test1", isDone = false });

            // Assert
            Assert.False(result.isDone);

            // Assert in database
            Assert.False(_context.Todos.Single(x => x.Id == result.Id).isDone);
        }

        [Fact]
        public async void Toggle_Notes_Returns_Correct_Details()
        {
            // Arrange
            await ResetContext();
            var todo1 = new Todo { Id = 12345, Text = "Test1", isDone = true };
            var todo2 = new Todo { Id = 12345, Text = "Test2", isDone = false };
            var service = new TodoService(_context);
            service.AddTodo(todo1);
            service.AddTodo(todo2);

            // Act/Assert

            // Toggle once to complete all
            var result = await service.ToggleNotes();
            Assert.All(result, x => Assert.True(x.isDone));

            //Toggle again to make all incomplete
            var result2 = await service.ToggleNotes();
            Assert.All(result2, x => Assert.False(x.isDone));
        }

        [Fact]
        public async void Clear_Completed_Returns_Correct_Details()
        {
            // Arrange
            await ResetContext();
            var todo1 = new Todo { Id = 12345, Text = "Test1", isDone = true };
            var todo2 = new Todo { Id = 12345, Text = "Test2", isDone = false };
            var service = new TodoService(_context);
            service.AddTodo(todo1);
            service.AddTodo(todo2);

            // Act
            service.ClearCompleted();

            // Assert
            Assert.Single(_context.Todos);
            Assert.Equal(todo2.Text, _context.Todos.Single().Text);
        }
    }
}