using System.Collections.Generic;

namespace TodoList.Shared.Dto
{
    
    public class ListOfTodosDto
    {
       
        public int Id { get; set; }
      
        public string Title { get; set; }
       
        public IEnumerable<TodoDto> Todos { get; set; }
       
        public int UserId { get; set; }
    }
}
