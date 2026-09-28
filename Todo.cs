public class Todo 
{
public int Id {get; set;}
public string Name {get; set;}
public StatusType Status{get; set;}
public List<int> AssignedUserIds {get; set; } = new List<int>();

public DateTime? StartDate {get; set;}
public DateTime? EndDate {get; set;}


public void ChangeStatus(StatusType status)
{
    Status = status;

    if (status == StatusType.Started)
    {
        StartDate = DateTime.Now;
    }
    else if (status == StatusType.Finished)
    {
        EndDate = DateTime.Now;
    }
}
}


public enum StatusType
{
    Unassigned,
    Assigned,
    Started,
    Finished
}

