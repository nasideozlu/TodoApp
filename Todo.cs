public class Todo 
{
public int Id {get; set;}
public string Name {get; set;} = string.Empty;
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
public static void AddTodoMenu(TodoService todoService,List<Todo> todos,int nextId)
{
    Console.WriteLine("Gorev adini yaziniz:");
    string? gorevAdi = Console.ReadLine();

    Todo? yeniGorev = todoService.AddTodo(
        todos,
        nextId,
        gorevAdi
    );

    if (yeniGorev != null)
    {
        nextId++;
        Console.WriteLine("Gorev eklendi.");
    }
    else
    {
        Console.WriteLine("Gorev adi bos olamaz.");
    }
}

public static void ShowTodos(TodoService todoService, UserService userService, List<Todo> todos, List<User> users)
{
    Console.WriteLine("Mevcut Gorevler:");
    
    foreach(Todo gorev in todos)
    {
        Console.WriteLine($"Id: {gorev.Id} , Gorev: {gorev.Name} , Durum: {gorev.Status}");
        Console.WriteLine($"Baslangic: {gorev.StartDate?.ToString("dd.MM.yyyy HH:mm") ?? "Baslamadi"}");
        Console.WriteLine($"Bitis: {gorev.EndDate?.ToString("dd.MM.yyyy HH:mm") ?? "Bitmedi"}");
        
        foreach (int userId in gorev.AssignedUserIds)
        {
            foreach (User kullanici in users)
            {
                if (kullanici.Id == userId)
                {
                    Console.WriteLine(
                        $"Atanan kullanici: {kullanici.Name}"
                    );
                }
            }
        }
    }
    
}

public static void DeleteTodoMenu(TodoService todoService, UserService userService, List<Todo> todos, List<User> users)
{
    Todo.ShowTodos(todoService, userService, todos, users);

    Console.WriteLine("Silmek istediginiz gorev Id seciniz:");

    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Gecersiz Id.");
        return;
    }

    bool silindi = todoService.DeleteTodo(todos, id);

    if (silindi)
    {
        Console.WriteLine("Gorev silindi.");
    }
    else
    {
        Console.WriteLine("Gorev Bulunamadi.");
    }
}

public static void CompleteTodoMenu(TodoService todoService, UserService userService, List<Todo> todos, List<User> users)
{
    Todo.ShowTodos(todoService, userService, todos, users);

    Console.WriteLine("Tamamlanan gorev id seciniz:");

    if (!int.TryParse(Console.ReadLine(), out int finishId))
    {
        Console.WriteLine("Gecersiz Id.");
        return;
    }

    bool tamamlandi = todoService.CompleteTodo(todos, finishId);

    if (tamamlandi)
    {
        Console.WriteLine("Gorev Tamamlandi.");
    }
    else
    {
        Console.WriteLine("Gorev bulunamadi veya baslatilmamis.");
    }
}

public static void AssignTodoMenu(TodoService todoService, UserService userService, List<Todo> todos, List<User> users)
{
    Todo.ShowTodos(todoService, userService, todos, users);

    Console.WriteLine("Atama yapilacak gorev Id giriniz:");

    if (!int.TryParse(Console.ReadLine(), out int gorevId))
    {
        Console.WriteLine("Gecersiz Id.");
        return;
    }

    User.ShowUsers(userService,users);

    Console.WriteLine("Atama yapilacak kullanici Id giriniz:");

    if (!int.TryParse(Console.ReadLine(), out int kullaniciId))
    {
        Console.WriteLine("Gecersiz Id.");
        return;
    }

    bool atandi = todoService.AssignTodo(
        todos,
        users,
        gorevId,
        kullaniciId
    );

    if (atandi)
    {
        Console.WriteLine("Gorev kullaniciya atandi.");
    }
    else
    {
        Console.WriteLine("Gorev veya kullanici bulunamadi ya da kullanici zaten atanmis.");
    }
}

public static void StartTodoMenu(TodoService todoService, UserService userService, List<Todo> todos, List<User> users)
{
    Todo.ShowTodos(todoService, userService, todos, users);

    Console.WriteLine("Baslatilacak gorev Id giriniz.");

    if (!int.TryParse(Console.ReadLine(), out int startId))
    {
        Console.WriteLine("Gecersiz Id.");
        return;
    }

    bool baslatildi = todoService.StartTodo(todos, startId);

    if (baslatildi)
    {
        Console.WriteLine("Gorev Baslatildi.");
    }
    else
    {
        Console.WriteLine("Gorev bulunamadi veya atanmamis.");
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

