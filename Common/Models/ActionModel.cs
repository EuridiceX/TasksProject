namespace CommonLibrary.Models
{
    public class ActionModel<T>
    {
        public ActionType Action { get; set; }
        public T Data { get; set; }
    }
    public enum ActionType
    {
        UpdateStatus,
        CreateEntity
    }

    public class UpdateModel
    {
        public int Id { get; set; }
        public TaskStatusEnum Status { get; set; }
    }

    public class TaskModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? AssignedTo { get; set; }
        public TaskStatusEnum? Status { get; set; }
    }

    public enum TaskStatusEnum
    {
        NotStarted,
        InProgress,
        Completed
    }
}
