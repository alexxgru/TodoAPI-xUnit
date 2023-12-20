using BackendAPI1.data;
using BackendAPI1.models;

namespace BackendAPI1.Controllers
{
    public class TodoService
    {

        private TodosContext _context;

        public TodoService(TodosContext context)
        {
            _context = context;
        }

        public Todo[] GetNotes(bool? completed)
        {
            if (completed == true)
            {
                return _context.Todos.Where(x => x.isDone).ToArray();
            }
            else if (completed == false)
            {
                return _context.Todos.Where(x => !x.isDone).ToArray();

            }
            else
            {
                return _context.Todos.ToArray();
            }
        }

        public Todo AddTodo(Todo todo)
        {
            if (string.IsNullOrWhiteSpace(todo.Text) || todo is null)
            {
                throw new NullReferenceException("Invalid todo");
            }

            Todo newTodo = new Todo { Text = todo.Text, isDone = todo.isDone };
            _context.Todos.Add(newTodo);
            _context.SaveChanges();

            return newTodo;
        }

        public void ToggleNotes()
        {
            var Todos = _context.Todos.ToArray();
            if (Todos.All(x => x.isDone))
            {
                foreach (var todo in Todos)
                {
                    todo.isDone = false;
                }
            }
            else
            {
                foreach (var todo in Todos)
                {
                    todo.isDone = true;
                }
            }
            _context.SaveChanges();
        }

        public void ClearCompleted()
        {
            var todos = _context.Todos.Where(x => x.isDone).ToArray();
            _context.Todos.RemoveRange(todos);
            _context.SaveChanges();
        }

        public void ChangeNote(Todo changeTodo)
        {
            var todo = _context.Todos.FirstOrDefault(x => x.Id == changeTodo.Id);

            if (todo == null)
            {
                throw new Exception("");
            }

            todo.isDone = changeTodo.isDone;


            _context.SaveChanges();
        }

        public Todo DeleteNote(int Id)
        {
            var todo = _context.Todos.FirstOrDefault(x => x.Id == Id);

            if (todo == null)
            {
                throw new Exception("");
            }

            _context.Remove(todo);
            _context.SaveChanges();
            return todo;
        }
    }
}
