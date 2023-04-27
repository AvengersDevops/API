namespace AvengersAPI.Models;

public class TaskToUserAssociation
{
    public TaskToUserAssociation(Entities.Task task, int userId)
    {
        UserId = userId;
        Task = task;
    }
    
    public Entities.Task Task { get; set; }

    public int UserId { get; }
}