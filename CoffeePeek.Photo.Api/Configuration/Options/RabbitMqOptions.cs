namespace CoffeePeek.Photo.Api.Configuration;

public class RabbitMqOptions
{
    public string HostName { get; set; }
    public ushort Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}