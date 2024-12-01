public class Solution
{
    private static readonly int[] _daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    private static bool IsLeapYear(int year)
    {
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }

    public static int GetDaysCount(string date)
    {
        string[] parts = date.Split('-');
        int year = int.Parse(parts[0]);
        int month = int.Parse(parts[1]);
        int day = int.Parse(parts[2]);

        int days = 0;

        for (int y = 1900; y < year; y++)
        {
            days += IsLeapYear(y) ? 366 : 365;
        }

        for (int m = 1; m < month; m++)
        {
            days += _daysInMonth[m - 1];
            if (m == 2 && IsLeapYear(year))
            {
                days += 1;  // February in a leap year has 29 days
            }
        }

        days += day;

        return days;
    }

    public int DaysBetweenDates(string date1, string date2)
    {
        int days1 = GetDaysCount(date1);
        int days2 = GetDaysCount(date2);

        return Math.Abs(days1 - days2);
    }
}