using TodoList.Shared.Enums;

namespace TodoList.Shared.Dto
{
    public class TodoForUpdateDto : TodoForManipulationDto
    {
      
        public bool IsDone { get; set; }
     
        public TodoColor Color { get; set; }

    }
}
