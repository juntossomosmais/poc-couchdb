namespace WebAPI.APIs.Banners;

public class Payloads
{
    public record Create(string Title, string ImagePath, int Order, string CallToAction, string Author);
}