public enum AppMessage
{
    ExitMessage,
    InvalidSelection
}

public static class AppMessages
{
    public static string GetMessage(AppMessage message)
    {
        return message switch
        {
            AppMessage.ExitMessage => "Cikis Yapiliyor.",
            AppMessage.InvalidSelection => "Hatali Secim Yaptiniz!",
            _ => string.Empty
        };
    }
}