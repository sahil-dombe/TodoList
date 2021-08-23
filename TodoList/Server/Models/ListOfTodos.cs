using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Server.Models
{
    public class ListOfTodos
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)] 
         
        public string Title { get; set; }
        public IEnumerable<Todo> Todos { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
