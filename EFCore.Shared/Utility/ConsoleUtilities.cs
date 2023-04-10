using EFCore.Model;

namespace EFCore.Shared.Utility;
public static class ConsoleUtilities
{
    public static DateTime ReadDateTime(string? message = null)
    {
        do
        {
            Console.Write(message);
            string? dateString = Console.ReadLine();
            if (DateTime.TryParse(dateString, out var date))
                return date;
            Console.Write("Unable to comprehend input as DateTime value. Try again");
        } while (true);
    }
}
