namespace CoffeePeek.Shared.Models.PhotoUpload;

public interface IPhotoUploadRequested
{
    public int UserId { get; set; }
    public int ShopId { get; set; }
    public ICollection<byte[]> Photos { get; set; }
}