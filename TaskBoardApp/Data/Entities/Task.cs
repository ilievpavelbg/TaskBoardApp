using System.ComponentModel.DataAnnotations;

namespace TaskBoardApp.Data.Entities
{
    public class Task
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.Task.MaxTaskTitle)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DataConstants.Task.MaxTaskDescription)]
        public string Description { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public int BoardId { get; set; }
        public Board Board { get; set; } = null!;

        [Required]
        public string OwnerId { get; set; } = null!;
        public User Owner { get; set; } = null!;
    }
}
