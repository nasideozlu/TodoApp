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
    Todo.ShowTodos(todoService, userService, todos, users);
    break;

case "2":
    Todo.AddTodoMenu(todoService,todos,nextId);
    break;

case"3":
    Todo.DeleteTodoMenu(todoService, userService,todos,users);
    break;

case "4":
    Todo.CompleteTodoMenu(todoService, userService, todos, users);
    break;

case "5":
    bool kullaniciEklendi = userService.AddUser(users, nextUserId);

    if (kullaniciEklendi)
    {
        nextUserId++;
    }

    break;

case "6":
    User.ShowUsers(userService, users);
    break;

case "7":
    Todo.AssignTodoMenu(todoService,userService,todos,users);
    break;

case "8":
    Todo.StartTodoMenu(todoService, userService, todos, users);
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


    User? LoginUser(List<User> users, UserService userService)
{
    if (users.Count == 0)
    {
        return userService.RegisterFirstUser(users);
    }

    return userService.Login(users);
}