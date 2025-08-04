using RestaurantManagementSystem.Domain.Enums;

namespace Restaurant_Management_System.Extentions
{
    public static class EnumExtensions
    {
        public static string ToFriendlyString(this OrderStatus status)
        {
            return status switch
            {
                OrderStatus.Pending => "Pending",
                OrderStatus.Preparing => "Preparing",
                OrderStatus.Ready => "Ready",
                OrderStatus.Completed => "Delivered",
                OrderStatus.Cancelled => "Cancelled",
                _ => status.ToString()
            };
        }

        public static string GetBadgeClass(this OrderStatus status)
        {
            return status switch
            {
                OrderStatus.Pending => "badge bg-warning",
                OrderStatus.Preparing => "badge bg-info",
                OrderStatus.Ready => "badge bg-success",
                OrderStatus.Completed => "badge bg-primary",
                OrderStatus.Cancelled => "badge bg-danger",
                _ => "badge bg-secondary"
            };
        }
    }
}
