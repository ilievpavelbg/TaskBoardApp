using System.ComponentModel.DataAnnotations;

namespace TaskBoardApp.Data.Entities
{
    public class Board
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.Board.MaxBoardName)]
        public string Name { get; set; } = null!;
        public ICollection<Task> Tasks { get; set; }
    }
}
