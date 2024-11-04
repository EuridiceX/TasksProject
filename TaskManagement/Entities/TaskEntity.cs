namespace TaskManagement.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public string Description { get; set; }
        public string? AssignedTo { get; set; }
        public int Status { get; set; }
    }
}
