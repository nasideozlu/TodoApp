using System.Linq;

List<Todo> todos = DataStorage.LoadTodos();
List<User> users = DataStorage.LoadUsers();
UserService userService = new UserService();

User? girisYapanKullanici = null;

if (users.Count == 0)
{
    girisYapanKullanici = userService.RegisterFirstUser(users);

    if (girisYapanKullanici == null)
    {
        return;
    }
}
else
{
    girisYapanKullanici = userService.Login(users);

    if (girisYapanKullanici == null)
    {
        return;
    }
}


foreach (Todo gorev in todos)
{
    if (gorev.AssignedUserIds.Count > 0 &&
        gorev.Status == StatusType.Unassigned)
    {
        gorev.ChangeStatus(StatusType.Assigned);
    }
}
DataStorage.SaveTodos(todos);

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
    Console.WriteLine("Gorev adini yaziniz:");
    var gorevAdi = Console.ReadLine();
    Todo yeniGorev = new Todo();
    yeniGorev.Id = nextId;
    yeniGorev.Name = gorevAdi;
    yeniGorev.Status = StatusType.Unassigned;
    todos.Add(yeniGorev);
    DataStorage.SaveTodos(todos);
    nextId++;
    break;

case"3":
    ShowTodos(todos, users);

    Console.WriteLine("Silmek istediginiz gorevi seciniz:");
   
    int id = Convert.ToInt32(Console.ReadLine());
    
    Todo? silinecek = FindTodo(todos, id );

    if (silinecek != null)
    {
         todos.Remove(silinecek);
         DataStorage.SaveTodos(todos);

         Console.WriteLine("Gorev silindi.");
    }
    else
    {
        Console.WriteLine("Gorev Bulunamadi.");
    }
    break;

case "4":
    ShowTodos(todos, users);

    Console.WriteLine("Tamamlanan gorev id seciniz:");
    int finishId = Convert.ToInt32(Console.ReadLine());
    
    Todo? guncelleme = FindTodo(todos, finishId);

    if(guncelleme != null)
    {
        if (guncelleme.Status != StatusType.Started)
        {
            Console.WriteLine("Yalnizca baslatilmis gorevler tamamlanabilir.");
            break;
        }
        
        guncelleme.ChangeStatus(StatusType.Finished);
        DataStorage.SaveTodos(todos);
        Console.WriteLine("Gorev Tamamlandi.");
    }
    else 
    {
        Console.WriteLine("Gorev Bulunamadi.");
    }
    
    break;

case "5":
    Console.WriteLine("Isim Giriniz:");
    var isim = Console.ReadLine();

    Console.WriteLine("Kullanici Adi Giriniz:");
    var kullaniciAdi = Console.ReadLine();
    if (users.Any(u => u.UserName == kullaniciAdi))
    {
        Console.WriteLine("Bu kullanici adi kullaniliyor.");
        break;
    }

    Console.WriteLine("Sifre Giriniz:");
    var sifre = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(isim) ||
    string.IsNullOrWhiteSpace(kullaniciAdi) ||
    string.IsNullOrWhiteSpace(sifre))
{
    Console.WriteLine("Tum alanlar zorunludur.");
    break;
}

    User yeniKullanici = new User();
    yeniKullanici.Id = nextUserId;
    yeniKullanici.Name = isim;
    yeniKullanici.UserName = kullaniciAdi;
    yeniKullanici.PasswordHash =  PasswordHasher.HashPassword(sifre);
    users.Add(yeniKullanici);
    DataStorage.SaveUsers(users);
    nextUserId++;

    Console.WriteLine("Kullanici eklendi.");
    break;

case "6":
    ShowUsers(users);
    break;

case "7":
    
    ShowTodos(todos, users);

    Console.WriteLine("Atama yapilacak gorev Id giriniz.");
    int gorevId = Convert.ToInt32(Console.ReadLine());

    Todo? atananGorev = FindTodo(todos, gorevId);

    if (atananGorev == null)
    {
        Console.WriteLine("Gorev bulunamadi.");
        break;
    }
    
    ShowUsers(users);
    
    Console.WriteLine("Atama yapilacak kullanici Id giriniz.");
    int kullaniciId= Convert.ToInt32(Console.ReadLine());

    User? gorevliKullanici = FindUser(users, kullaniciId);

    if ( gorevliKullanici == null)
    {
        Console.WriteLine("Kullanici bulunamadi.");
        break;
    }
        if (atananGorev.AssignedUserIds.Contains(gorevliKullanici.Id))
        {
            Console.WriteLine("Bu kullanici zaten bu goreve atanmis.");
            break;
        }
    atananGorev.AssignedUserIds.Add(gorevliKullanici.Id);
    atananGorev.ChangeStatus(StatusType.Assigned);
    DataStorage.SaveTodos(todos);
    Console.WriteLine("Gorev kullaniciya atandi.");

    break;

case "8":
    ShowTodos(todos, users);

    Console.WriteLine("Baslatilacak gorev Id giriniz.");
    int startId = Convert.ToInt32(Console.ReadLine());

    Todo? baslatilacakGorev = FindTodo(todos, startId);
    if (baslatilacakGorev == null)
    {
        Console.WriteLine("Gorev bulunamadi.");
        break;
    }
    if (baslatilacakGorev.Status != StatusType.Assigned)
    {
        Console.WriteLine("Atanmamis gorev baslatilamaz.");
        break;
    }

    baslatilacakGorev.ChangeStatus(StatusType.Started);
    DataStorage.SaveTodos(todos);

    Console.WriteLine("Gorev Baslatildi.");
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

Todo? FindTodo (List<Todo> todos, int id)
{
    foreach (Todo gorev in todos)
    {
        if (gorev.Id == id)
        {
            return gorev;
        }
    }
    
    return null;
}
User? FindUser (List<User> users, int id)
{
    foreach(User kullanici in users)
    {
        if (kullanici.Id == id)
        {
            return kullanici;
        }
    }

    return null;
}

void ShowUsers(List<User> users )
{
    Console.WriteLine("Mevcut Kullanicilar:");
    foreach (User kullanici in users)
    {
        Console.WriteLine($"Id: {kullanici.Id} , Isim: {kullanici.Name}");
    }
}