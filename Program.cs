using System.Linq;

List<Todo> todos = DataStorage.LoadTodos();
List<User> users = DataStorage.LoadUsers();
UserService userService = new UserService();
TodoService todoService = new TodoService();

User? girisYapanKullanici = LoginUser(users, userService);

if (girisYapanKullanici == null)
{
    return;
}

todoService.UpdateAssignedTodoStatuses(todos);

int nextId = todos.Count > 0
    ? todos.Max( t => t.Id) + 1 
    : 1;
int nextUserId= users.Count > 0
    ? users.Max( u => u.Id) + 1 
    : 1;
bool shouldExit = false;
while (!shouldExit)
{

SystemText();
var kullaniciSecimi = Console.ReadLine();

switch(kullaniciSecimi)
{
case "1":
    ShowTodos(todos, users);
    break;

case "2":
    
    AddTodoMenu();
    break;

case"3":
    DeleteTodoMenu();
    break;

case "4":
    CompleteTodoMenu();
    break;

case "5":
    bool kullaniciEklendi = userService.AddUser(users, nextUserId);

    if (kullaniciEklendi)
    {
        nextUserId++;
    }

    break;

case "6":
    ShowUsers(users);
    break;

case "7":
    AssignTodoMenu();
    break;

case "8":
    StartTodoMenu();
    break;

case "9":
    Console.WriteLine("Cikis Yapiliyor.");
    shouldExit = true;
    break;

default:
    Console.WriteLine("Hatali Secim Yaptiniz");
    break;
}

}

void SystemText()
{

Console.WriteLine("Yapmak Istediginiz Islemi Secin");
Console.WriteLine("1.Gorevleri Listele");
Console.WriteLine("2.Gorev Ekle");
Console.WriteLine("3.Gorev Sil");
Console.WriteLine("4.Gorev Durumu Guncelle");
Console.WriteLine("5.Kullanici Ekle");
Console.WriteLine("6.Kullanicilari Listele");
Console.WriteLine("7.Gorev Atamasi Yap");
Console.WriteLine("8.Gorev Baslat");
Console.WriteLine("9.Cikis Yap");

}

void ShowTodos(List<Todo> todos, List<User> users )
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

void ShowUsers(List<User> users )
{
    Console.WriteLine("Mevcut Kullanicilar:");
    foreach (User kullanici in users)
    {
        Console.WriteLine($"Id: {kullanici.Id} , Isim: {kullanici.Name}");
    }

}
void AddTodoMenu()
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
void DeleteTodoMenu()
{
    ShowTodos(todos, users);

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

void CompleteTodoMenu()
{
    ShowTodos(todos, users);

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

void AssignTodoMenu()
{
    ShowTodos(todos, users);

    Console.WriteLine("Atama yapilacak gorev Id giriniz:");

    if (!int.TryParse(Console.ReadLine(), out int gorevId))
    {
        Console.WriteLine("Gecersiz Id.");
        return;
    }

    ShowUsers(users);

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
void StartTodoMenu()
{
    ShowTodos(todos, users);

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
User? LoginUser(List<User> users, UserService userService)
{
    if (users.Count == 0)
    {
        return userService.RegisterFirstUser(users);
    }

    return userService.Login(users);
}