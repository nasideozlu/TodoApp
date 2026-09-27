public class Todo 
{
public int Id {get; set;}
public string Name {get; set;}
public bool Completed {get; set;}  
public List<int> AssignedUserIds {get; set; } = new List<int>();
}