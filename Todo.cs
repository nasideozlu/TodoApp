public class Todo 
{
public int Id {get; set;}
public string Name {get; set;}
public StatusType Status{get; set;}
public List<int> AssignedUserIds {get; set; } = new List<int>();

public void ChangeStatus( StatusType status)
{
    Status = status;
}
}

public enum StatusType
{
    Unassigned,
    Assigned,
    Started,
    Finished
}

