namespace CoffeePeek.Shared.Models.PhotoUpload;

public interface IPhotoUploadResult
{
    public int UserId { get; set; }
    public int ShopId { get; set; }
    public ICollection<string> PhotoUrls { get; set; }
}