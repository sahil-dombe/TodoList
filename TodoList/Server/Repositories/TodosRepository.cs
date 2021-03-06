using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Server.Models;

namespace TodoList.Server.Repositories
{
    public class TodosRepository : ITodosRepository
    {
        private readonly TodoContext _context;

        public TodosRepository(TodoContext context)
        {
            _context = context;
        }
         
        public async Task<Todo> GetTodoAsync(int listOfTodosId, int todoId)
        {
            return await _context.Todos.FirstOrDefaultAsync(t => t.ListOfTodosId == listOfTodosId && t.Id == todoId);
        }

        public async Task<IEnumerable<Todo>> GetTodosAsync(int listOfTodosId)
        {
            return await _context.Todos.Where(t => t.ListOfTodosId == listOfTodosId).ToListAsync();
        }

        public async Task<bool> TodoExists(int listOfTodosId, string title, int? todoId = null)
        {
            return await _context.Todos.AnyAsync(t => t.Id != todoId && t.ListOfTodosId == listOfTodosId && t.Title == title);
        }

        public void UpdateTodo(Todo todo)
        {
            
        }
    }
}
