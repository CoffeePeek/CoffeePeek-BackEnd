namespace ClassLibrary.SharedModels;

public interface IPhotoUploadRequested
{
    public int UserId { get; set; }
    public int ShopId { get; set; }
    public ICollection<byte[]> Photos { get; set; }
}