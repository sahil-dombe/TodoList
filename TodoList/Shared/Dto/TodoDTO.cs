using System;
using TodoList.Shared.Enums;

namespace TodoList.Shared.Dto
{
  
    public class TodoDto
    {
       
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
    
        public bool IsDone { get; set; }
        
        public DateTime DateAdded { get; set; }
      
        public TodoColor Color { get; set; }
        
        public int ListOfTodosId { get; set; }

    }
}
