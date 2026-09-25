// See https://aka.ms/new-console-template for more information
List<Todo> todos = new List<Todo>();
int nextId = 1;
bool shouldExit = false;
while (!shouldExit)
{
Console.WriteLine("Yapmak Istediginiz Islemi Secin");
Console.WriteLine("1.Gorevleri Listele");
Console.WriteLine("2.Gorev Ekle");
Console.WriteLine("3.Gorev Sil");
Console.WriteLine("4.Gorev Durumu Guncelle");
Console.WriteLine("5.Cikis Yap");

var kullaniciSecimi = Console.ReadLine();

switch(kullaniciSecimi)
{
case "1":
    Console.WriteLine("Mevcut Gorevler:");
    
    foreach(Todo gorev in todos)
    {
         Console.WriteLine($"Id: {gorev.Id} , Gorev: {gorev.Name} , Tamamlandi: {gorev.Completed}");
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
    nextId++;
    break;

case"3":
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
    }
    break;

case "4":
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
        Console.WriteLine("Gorev Tamamlandi.");
    }
    else 
    {
        Console.WriteLine("Gorev Bulunamadi.");
    }
    break;


case "5":
    Console.WriteLine("Cikis Yapiliyor.");
    shouldExit = true;
    break;

default:
    Console.WriteLine("Hatali Secim Yaptiniz");
    break;
}

}