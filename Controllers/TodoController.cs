using BackendAPI1.data;
using BackendAPI1.models;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI1.Controllers
{
    [ApiController]
    [Route("/notes")]
    public class TodoController : Controller
    {

        private TodosContext _context;

        public TodoController(TodosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public Todo[] OnGet(bool? completed)
        {
            return new TodoService(_context).GetNotes(completed);
        }

        [HttpGet, Route("/remaining")]
        public int OnGetRemaining()
        {
            return _context.Todos.Count();
        }

        public Todo _todo;

        [HttpPost]
        public IActionResult OnPost([FromBody] Todo todo)
        {
            try
            {
                new TodoService(_context).AddTodo(todo);
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost, Route("/toggle-all")]
        public IActionResult OnTogglePost()
        {
            try
            {
                new TodoService(_context).ToggleNotes();
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost, Route("/clear-completed")]
        public IActionResult OnPostClear()
        {
            try
            {
                new TodoService(_context).ClearCompleted();
                return Ok();

            }
            catch
            {
                return StatusCode(500);
            }

        }

        [HttpPut("{Id:int}")]
        public IActionResult OnPut([FromBody] Todo changeTodo)
        {
            try
            {
                new TodoService(_context).ChangeNote(changeTodo);
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{Id:int}")]
        public IActionResult OnDelete(int Id)
        {
            try
            {
                new TodoService(_context).DeleteNote(Id);
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }

        }

    }
}
