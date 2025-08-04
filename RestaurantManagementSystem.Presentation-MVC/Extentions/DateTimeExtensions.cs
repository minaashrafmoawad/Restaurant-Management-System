namespace Restaurant_Management_System.Extentions
{
    public static class DateTimeExtensions
    {
        public static string ToFriendlyString(this DateTime dateTime)
        {
            return dateTime.ToString("MMM dd, yyyy hh:mm tt");
        }

        public static string ToTimeString(this TimeSpan timeSpan)
        {
            return $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}";
        }
    }
}
