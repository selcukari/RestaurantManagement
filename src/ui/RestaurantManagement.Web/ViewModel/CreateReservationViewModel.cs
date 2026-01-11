using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagement.Web.ViewModel
{
    public record CreateReservationViewModel
    {
        public static CreateReservationViewModel Empty => new();

        [Display(Name = "Product Table")] public SelectList TableDropdownList { get; set; } = null!;

        [Display(Name = "ReservationDate")] public DateTime ReservationDate { get; init; }

        [Display(Name = "StartTime")] public TimeSpan StartTime { get; init; }

        [Display(Name = "EndTime")] public TimeSpan EndTime { get; init; }

        [Display(Name = "Misafir Sayısı")] public int GuestCount { get; init; }

        public Guid TableId { get; init; }

        public void SetTableDropdownList(List<TableViewModel> tables)
        {
            TableDropdownList = new SelectList(tables, "Id", "DisplayText");
        }
    }
}
