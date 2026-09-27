using System.Text.Json;


public class DataStorage
{
    public static void SaveTodos (List<Todo> todos)
    {
        string json = JsonSerializer.Serialize(todos);
        File.WriteAllText("todos.json", json);
    }

    public static void SaveUsers(List<User> users)
    {
        string json = JsonSerializer.Serialize(users);
        File.WriteAllText("users.json", json);
    }

    public static List<Todo> LoadTodos()
    {
        if (!File.Exists("todos.json"))
        {
            return new List<Todo>();
        }

     string json = File.ReadAllText("todos.json");

        return JsonSerializer.Deserialize<List<Todo>>(json)
           ?? new List<Todo>();
    }

    public static List<User> LoadUsers()
    {
        if (!File.Exists("users.json"))
        {
         return new List<User>();
        }
    
        string json = File.ReadAllText("users.json");

        return JsonSerializer.Deserialize<List<User>>(json)
           ?? new List<User>();
    }
}

