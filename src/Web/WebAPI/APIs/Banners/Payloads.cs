namespace WebAPI.APIs.Banners;

public static class Payloads
{
    public record CreateBanner(string Title, string ImagePath, int Order, string CallToAction, string Author);
}