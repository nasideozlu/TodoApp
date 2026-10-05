public class TodoService
{
    public Todo? AddTodo(List<Todo> todos, int nextId, string? gorevAdi)
    {
        if (string.IsNullOrWhiteSpace(gorevAdi))
        {
            return null;
        }

        Todo yeniGorev = new Todo
        {
            Id = nextId,
            Name = gorevAdi,
            Status = StatusType.Unassigned
        };

        todos.Add(yeniGorev);
        DataStorage.SaveTodos(todos);

        return yeniGorev;
    }
    public bool DeleteTodo(List<Todo> todos, int id)
    {
        Todo? silinecek = FindTodo(todos, id);

        if (silinecek == null)
        {
            return false;
        }

        todos.Remove(silinecek);
        DataStorage.SaveTodos(todos);

        return true;
    }
    public bool CompleteTodo(List<Todo> todos, int id)
    {
        Todo? gorev = FindTodo(todos, id);

        if (gorev == null)
        {
          return false;
        }

        if (gorev.Status != StatusType.Started)
        {
            return false;
        }

        gorev.ChangeStatus(StatusType.Finished);
        DataStorage.SaveTodos(todos);

        return true;
    }
    
    public bool AssignTodo(List<Todo> todos,List <User> users,int todoId,int userId)
    {
    Todo? gorev = FindTodo(todos, todoId);

    if (gorev == null)
    {
        return false;
    }
    User? kullanici = FindUser(users, userId);

    if (kullanici == null)
    {
        return false;
    }

    if (gorev.AssignedUserIds.Contains(userId))
    {
        return false;
    }

    gorev.AssignedUserIds.Add(userId);
    gorev.ChangeStatus(StatusType.Assigned);

    DataStorage.SaveTodos(todos);

    return true;
    }

    public bool StartTodo(List<Todo> todos, int id)
    {
        Todo? gorev = FindTodo(todos, id);

        if (gorev == null)
        {
            return false;
        }
        if (gorev.Status != StatusType.Assigned)
        {
            return false;
        }

        gorev.ChangeStatus(StatusType.Started);
        DataStorage.SaveTodos(todos);
        return true;
    }

    public void UpdateAssignedTodoStatuses(List<Todo> todos)
    {
        foreach (Todo gorev in todos)
        {
            if (gorev.AssignedUserIds.Count > 0 &&
                gorev.Status == StatusType.Unassigned)
            {
                gorev.ChangeStatus(StatusType.Assigned);
            }
        }

        DataStorage.SaveTodos(todos);
    }
    private Todo? FindTodo(List<Todo> todos, int id)
    {
        return todos.FirstOrDefault(t => t.Id == id);
    }

    private User? FindUser(List<User> users, int id)
    {
        return users.FirstOrDefault(u => u.Id == id);
    }
}