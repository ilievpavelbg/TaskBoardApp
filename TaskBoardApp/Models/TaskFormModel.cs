namespace TaskBoardApp.Models
{
    public class TaskFormModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int BoardId { get; set; }
        public IEnumerable<TaskBoarModel> Boards { get; set; } = null!;
    }
}
