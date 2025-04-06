namespace ClassLibrary.SharedModels;

public class PhotosCreated
{
    public int UserId { get; set; }
    public int ShopId { get; set; }
    public ICollection<string> PhotoUrls { get; set; }
}