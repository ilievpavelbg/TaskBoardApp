namespace TaskBoardApp.Models
{
    public class BoardViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<TasksViewModel> Tasks { get; set; } = new List<TasksViewModel>();
    }
}
