using BackendAPI1.data;
using BackendAPI1.models;
using Microsoft.AspNetCore.Http;
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
                Todo newTodo = new Todo { Text = todo.Text, isDone = todo.isDone };
                _context.Todos.Add(newTodo);
                _context.SaveChanges();
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
               var todos = _context.Todos.Where(x => x.isDone).ToArray();
                _context.Todos.RemoveRange(todos);
                _context.SaveChanges();
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
                var todo = _context.Todos.FirstOrDefault(x => x.Id == changeTodo.Id);

                if (todo == null)
                {
                    throw new Exception("");
                }
                
                todo.isDone = changeTodo.isDone;


                _context.SaveChanges();
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
                var todo = _context.Todos.FirstOrDefault(x => x.Id == Id);

                if (todo == null)
                {
                    throw new Exception("");
                }

                _context.Remove(todo);

                _context.SaveChanges();
                return Ok();
            }
            catch 
            {
                return StatusCode(500);
            }

        }

    }
}
