namespace RestaurantManagement.Bus.Commands
{
   public record class UploadProductPictureCommand(Guid courseId, byte[] picture, string FileName);
}
