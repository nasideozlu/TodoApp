public class User
{
    public int Id {get; set;}
    public string Name {get; set;}= string.Empty;
    public string UserName {get; set;}= string.Empty;
    public string PasswordHash {get; set;}= string.Empty;



public static void ShowUsers(UserService userService, List<User> users )
{
    Console.WriteLine("Mevcut Kullanicilar:");
    foreach (User kullanici in users)
    {
        Console.WriteLine($"Id: {kullanici.Id} , Isim: {kullanici.Name}");
    }

}
    
}