using System.Linq;


List<Todo> todos = DataStorage.LoadTodos();
List<User> users = DataStorage.LoadUsers();
int nextId = todos.Count > 0
    ? todos.Max( t => t.Id) + 1 
    : 1;
int nextUserId= users.Count > 0
    ? users.Max( u => u.Id) + 1 
    : 1;
bool shouldExit = false;
while (!shouldExit)
{
Console.WriteLine("Yapmak Istediginiz Islemi Secin");
Console.WriteLine("1.Gorevleri Listele");
Console.WriteLine("2.Gorev Ekle");
Console.WriteLine("3.Gorev Sil");
Console.WriteLine("4.Gorev Durumu Guncelle");
Console.WriteLine("5.Kullanici Ekle");
Console.WriteLine("6.Kullanicilari Listele");
Console.WriteLine("7.Gorev Atamasi Yap");
Console.WriteLine("8.Cikis Yap");

var kullaniciSecimi = Console.ReadLine();

switch(kullaniciSecimi)
{
case "1":
    Console.WriteLine("Mevcut Gorevler:");
    
    foreach(Todo gorev in todos)
    {
         Console.WriteLine($"Id: {gorev.Id} , Gorev: {gorev.Name} , Tamamlandi: {gorev.Completed}");
        foreach(int userId in gorev.AssignedUserIds)
        {
            foreach(User kullanici in users)
            {
               if (kullanici.Id == userId)
               {
                Console.WriteLine($"Atanan kullanici: {kullanici.Name}");
               }
            }
        }


    }
    break;

case "2":
    Console.WriteLine("Gorev adini yaziniz:");
    var gorevAdi = Console.ReadLine();
    Todo yeniGorev = new Todo();
    yeniGorev.Id = nextId;
    yeniGorev.Name = gorevAdi;
    yeniGorev.Completed = false;
    todos.Add(yeniGorev);
    DataStorage.SaveTodos(todos);
    nextId++;
    break;

case"3":
    Console.WriteLine("Mevcut Gorevler:");
    
    foreach(Todo gorev in todos)
    {
         Console.WriteLine($"Id: {gorev.Id} , Gorev: {gorev.Name} , Tamamlandi: {gorev.Completed}");
    }

    Console.WriteLine("Silmek istediginiz gorevi seciniz:");
   
    int id = Convert.ToInt32(Console.ReadLine());
    Todo? silinecek = null;

    foreach( Todo gorev in todos)
    {
        if (gorev.Id == id)
        {
            silinecek = gorev;
        }
    
    }
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
    Console.WriteLine("Mevcut Gorevler:");
    
    foreach(Todo gorev in todos)
    {
         Console.WriteLine($"Id: {gorev.Id} , Gorev: {gorev.Name} , Tamamlandi: {gorev.Completed}");
    }
    Console.WriteLine("Tamamlanan gorev id seciniz:");
    int finishId = Convert.ToInt32(Console.ReadLine());
    Todo? guncelleme = null;

    foreach(Todo gorev in todos)
    {
        if(gorev.Id == finishId)
        {
            guncelleme = gorev;
        }
    }
    if(guncelleme != null)
    {
        guncelleme.Completed = true;
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

    Console.WriteLine("Sifre Giriniz:");
    var sifre = Console.ReadLine();

    User yeniKullanici = new User();
    yeniKullanici.Id = nextUserId;
    yeniKullanici.Name = isim;
    yeniKullanici.UserName = kullaniciAdi;
    yeniKullanici.Password = sifre;
    users.Add(yeniKullanici);
    DataStorage.SaveUsers(users);
    nextUserId++;

    Console.WriteLine("Kullanici eklendi.");
    break;

case "6":
    Console.WriteLine("Mevcut Kullanicilar:");
    
    foreach(User kullanici in users)
    {

         Console.WriteLine($"Id: {kullanici.Id} , Isim: {kullanici.Name} , Kullanici Adi: {kullanici.UserName}");
    }

    break;

case "7":
    
    Console.WriteLine("Mevcut Gorevler:");

    foreach (Todo gorev in todos)
    {
        Console.WriteLine($"Id:{gorev.Id} , Gorev: {gorev.Name}");
    }
    Console.WriteLine("Atama yapilacak gorev Id giriniz.");
    int gorevId = Convert.ToInt32(Console.ReadLine());

    Todo? atananGorev = null;
    foreach(Todo gorev in todos)
    {
        if (gorev.Id == gorevId) 
        {
            atananGorev = gorev;
        }
    }
    if(atananGorev == null )
    {
        Console.WriteLine("Gorev bulunamadi.");
    }
    Console.WriteLine("Mevcut Kullanicilar:");
    
    foreach(User kullanici in users)
    {

         Console.WriteLine($"Id: {kullanici.Id} , Isim: {kullanici.Name} ");
    }
    Console.WriteLine("Atama yapilacak kullanici Id giriniz.");
    int kullaniciId= Convert.ToInt32(Console.ReadLine());

    User? gorevliKullanici = null;
    
    foreach(User kullanici in users)
    {
        if(kullanici.Id == kullaniciId)
        {
            gorevliKullanici = kullanici;
            break;
        }
    }
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
    DataStorage.SaveTodos(todos);
    Console.WriteLine("Gorev kullaniciya atandi.");

    break;
    

case "8":
    Console.WriteLine("Cikis Yapiliyor.");
    shouldExit = true;
    break;

default:
    Console.WriteLine("Hatali Secim Yaptiniz");
    break;
}

}