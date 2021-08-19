using System.ComponentModel.DataAnnotations;

namespace TodoList.Shared.Dto
{
    public abstract class TodoForManipulationDto
    {
       
        [Required(AllowEmptyStrings = false)]
        [MaxLength(100, ErrorMessage = "The Title field may contain at most 100 characters.")]
        public string Title { get; set; }
       
        [MaxLength(500, ErrorMessage = "The Description field may contain at most 500 characters.")]
        public string Description { get; set; }
    }
}
