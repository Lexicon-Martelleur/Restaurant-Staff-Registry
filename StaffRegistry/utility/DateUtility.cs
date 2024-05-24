namespace StaffRegistry.utility;

internal static class DateUtility
{
    internal static long ConvertDateStringToTimeStamp(string dateString)
    {
        DateTime date;
        if (DateTime.TryParse(dateString, out date))
        {
            long unixTimestamp = ((DateTimeOffset)date).ToUnixTimeSeconds();
            return unixTimestamp;
        }
        else
        {
            throw new Exception("Invalid date format.");
        }
    }

    internal static string ConvertTimeStampToDateString(long unixTimestamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);
        DateTime dateTime = dateTimeOffset.DateTime;
        return $"{dateTime.ToString("yyyy-MM-dd")}";
    }

    internal static string ConvertTimeStampToDateStringLongFormat(long unixTimestamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);
        DateTime dateTime = dateTimeOffset.DateTime;
        return $"{dateTime.ToString("yyyy-MM-dd HH:mm:ss")}";
    }
}
