using System.Linq;


public class UserService
{
    public User? RegisterFirstUser(List<User> users)
    {
        Console.WriteLine("Ilk kullanici kaydi");

        Console.WriteLine("Isim Giriniz:");
        string? isim = Console.ReadLine();

        Console.WriteLine("Kullanici Adi Giriniz:");
        string? kullaniciAdi = Console.ReadLine();

        Console.WriteLine("Sifre Giriniz:");
        string? sifre = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(isim) ||
            string.IsNullOrWhiteSpace(kullaniciAdi) ||
            string.IsNullOrWhiteSpace(sifre))
        {
            Console.WriteLine("Tum alanlar zorunludur.");
            return null;
        }

        User yeniKullanici = new User
        {
            Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1,
            Name = isim,
            UserName = kullaniciAdi,
            PasswordHash = PasswordHasher.HashPassword(sifre)
        };

        users.Add(yeniKullanici);
        DataStorage.SaveUsers(users);

        Console.WriteLine($"Hos geldin {yeniKullanici.Name}!");

        return yeniKullanici;
    }
    public User? Login(List<User> users)
    {
        Console.WriteLine("Kullanici Adi:");
        string? girilenKullaniciAdi = Console.ReadLine();

        Console.WriteLine("Sifre:");
        string? girilenSifre = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(girilenKullaniciAdi) ||
            string.IsNullOrWhiteSpace(girilenSifre))
        {
            Console.WriteLine("Kullanici adi ve sifre bos olamaz.");
            return null;
        }

        User? kullanici = users.FirstOrDefault(u => u.UserName == girilenKullaniciAdi);

        if (kullanici == null ||
            !PasswordHasher.VerifyPassword(
                girilenSifre,
                kullanici.PasswordHash))
        {
            Console.WriteLine("Kullanici adi veya sifre hatali.");
            return null;
        }

        Console.WriteLine($"Hos geldin {kullanici.Name}!");

        return kullanici;
    }
}