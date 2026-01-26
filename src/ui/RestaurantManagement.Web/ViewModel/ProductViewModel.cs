namespace RestaurantManagement.Web.ViewModel
{
    public record ProductViewModel
    (
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string ImageUrl,
    string Created,
    int Quantity,
    string EducatorFullName,
    string MenuName,
    Guid? MenuId,
    int Calorie,
    float Rating)
    {
        public string TruncateDescription(int maxLength)
        {
            if (Description.Length <= maxLength) return Description;
            return Description.Substring(0, maxLength) + "...";
        }
    }
}
