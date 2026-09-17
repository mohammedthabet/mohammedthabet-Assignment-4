using System.Globalization;
using System.Text;
// ======================================================
// Part 1 — Starter Data
// ======================================================

string[] sessionNames =
{
    "C# Basics",
    "Arrays",
    "Functions",
    "Date and Time",
    "Exception Handling"
};

DateTime[] sessionDates =
{
    new DateTime(2026, 9, 10, 18, 0, 0),
    new DateTime(2026, 9, 13, 18, 0, 0),
    new DateTime(2026, 9, 17, 18, 0, 0),
    new DateTime(2026, 9, 20, 18, 0, 0),
    new DateTime(2026, 9, 24, 18, 0, 0)
};

int[] sessionDurations =
{
    180,
    240,
    180,
    240,
    180
};


// ======================================================
// Part 31 — Console Menu
// ======================================================

int choice;

do
{
    DisplayMenu();

    string? input = Console.ReadLine();

    if (!int.TryParse(input, out choice))
    {
        Console.WriteLine("Invalid input. Please enter a number.");
        continue;
    }

    Console.WriteLine();

    switch (choice)
    {
        case 1:
            DisplaySessions(sessionNames, sessionDates, sessionDurations);
            break;

        case 2:
            SearchSession(sessionNames, sessionDates, sessionDurations);
            break;

        case 3:
            SortSessionNames(sessionNames);
            break;

        case 4:
            ReverseSessionNames(sessionNames);
            break;

        case 5:
            FindSessionIndex(sessionNames);
            break;

        case 6:
            CheckSessionExists(sessionNames);
            break;

        case 7:
        {
            int total = GetTotalDuration(sessionDurations);
            double average = GetAverageDuration(sessionDurations);
            int shortest = GetShortestDuration(sessionDurations);
            int longest = GetLongestDuration(sessionDurations);

            Console.WriteLine($"Total Duration: {total} minutes");
            Console.WriteLine($"Average Duration: {average} minutes");
            Console.WriteLine($"Shortest Duration: {shortest} minutes");
            Console.WriteLine($"Longest Duration: {longest} minutes");

            SortDurations(sessionDurations);
            break;
        }

        case 8:
            DisplaySessionDetails(sessionNames, sessionDates, sessionDurations);
            break;

        case 9:
            ShowSessionStatus(sessionNames, sessionDates);
            break;

        case 10:
            FindNextSession(sessionNames, sessionDates);
            break;

        case 11:
            CompareSessionDates(sessionNames, sessionDates);
            break;

        case 12:
        {
            DateTime validDate = ReadSessionDate();
            Console.WriteLine($"Valid date: {validDate:dd MMMM yyyy hh:mm tt}");
            break;
        }

        case 13:
            ReadSessionByIndex(sessionNames);
            break;

        case 14:
        {
            Console.Write("Enter duration: ");

            if (!int.TryParse(Console.ReadLine(), out int duration))
            {
                Console.WriteLine("Invalid duration.");
                break;
            }

            try
            {
                ValidateDuration(duration);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            break;
        }

        case 15:
        {
            string report = BuildScheduleReport(
                sessionNames,
                sessionDates,
                sessionDurations);

            Console.WriteLine(report);
            break;
        }

        case 16:
        {
            string report = BuildScheduleReportWithStringBuilder(
                sessionNames,
                sessionDates,
                sessionDurations);

            Console.WriteLine(report);
            break;
        }

        case 0:
            Console.WriteLine("Goodbye!");
            break;

        default:
            Console.WriteLine("Invalid option. Please choose from 0 to 16.");
            break;
    }

} while (choice != 0);

// ======================================================
// FUNCTIONS
// ======================================================


// ======================================================
// Part 2 — Display All Sessions
// ======================================================

static void DisplaySessions(
    string[] names,
    DateTime[] dates,
    int[] durations)
{
    for (int i = 0;
         i < names.Length;
         i++)
    {
        Console.WriteLine(
            $"{i + 1}. {names[i]}");

        Console.WriteLine(
            $"Date: {dates[i]:dd MMMM yyyy}");

        Console.WriteLine(
            $"Start Time: {dates[i]:hh:mm tt}");

        Console.WriteLine(
            $"Duration: {durations[i]} minutes");

        Console.WriteLine();
    }
}


// ======================================================
// Part 3 — Search for a Session
// ======================================================

static void SearchSession(
    string[] names,
    DateTime[] dates,
    int[] durations)
{
    Console.Write(
        "Enter session name: ");

    string? searchName =
        Console.ReadLine();

    int index =
        Array.IndexOf(
            names,
            searchName);

    if (index == -1)
    {
        Console.WriteLine(
            "Session not found.");

        return;
    }

    Console.WriteLine(
        $"Name: {names[index]}");

    Console.WriteLine(
        $"Date: {dates[index]:dd MMMM yyyy}");

    Console.WriteLine(
        $"Start Time: {dates[index]:hh:mm tt}");

    Console.WriteLine(
        $"Duration: {durations[index]} minutes");
}


// ======================================================
// Part 4.1 — Sort Session Names
// Array.Copy() + Array.Sort()
// ======================================================

static void SortSessionNames(
    string[] names)
{
    string[] sortedNames =
        new string[names.Length];

    Array.Copy(
        names,
        sortedNames,
        names.Length);

    Array.Sort(sortedNames);

    Console.WriteLine(
        "Sorted session names:");

    foreach (string name in sortedNames)
    {
        Console.WriteLine(name);
    }
}


// ======================================================
// Part 4.2 — Reverse Session Names
// Array.Copy() + Array.Reverse()
// ======================================================

static void ReverseSessionNames(
    string[] names)
{
    string[] reversedNames =
        new string[names.Length];

    Array.Copy(
        names,
        reversedNames,
        names.Length);

    Array.Reverse(
        reversedNames);

    Console.WriteLine(
        "Reversed session names:");

    foreach (string name in reversedNames)
    {
        Console.WriteLine(name);
    }
}


// ======================================================
// Part 4.3 — Find Session Index
// Array.IndexOf()
// ======================================================

static void FindSessionIndex(
    string[] names)
{
    Console.Write(
        "Enter session name to find its index: ");

    string? searchName =
        Console.ReadLine();

    int index =
        Array.IndexOf(
            names,
            searchName);

    Console.WriteLine(
        $"Index: {index}");
}


// ======================================================
// Part 4.4 — Check if a Session Exists
// Array.Exists()
// ======================================================

static void CheckSessionExists(
    string[] names)
{
    Console.Write(
        "Enter session name to check: ");

    string? searchName =
        Console.ReadLine();

    bool exists =
        Array.Exists(
            names,
            name => name == searchName);

    if (exists)
    {
        Console.WriteLine(
            "Session exists.");
    }
    else
    {
        Console.WriteLine(
            "Session does not exist.");
    }
}


// ======================================================
// Part 4.5 — Find a Session
// Array.Find()
// ======================================================

static void FindSession(
    string[] names)
{
    string? foundSession =
        Array.Find(
            names,
            name => name.Contains("Date"));

    Console.WriteLine(
        $"Found session: {foundSession}");
}


// ======================================================
// Part 4.6 — Find a Session Index Using a Condition
// Array.FindIndex()
// ======================================================

static void FindSessionIndexByCondition(
    string[] names)
{
    int index =
        Array.FindIndex(
            names,
            name => name.Contains("Exception"));

    Console.WriteLine(
        $"Condition match index: {index}");
}


// ======================================================
// Part 4.7 — Copy an Array
// Array.Copy()
// ======================================================

static void CopySessionNames(
    string[] names)
{
    string[] copiedNames =
        new string[names.Length];

    Array.Copy(
        names,
        copiedNames,
        names.Length);

    copiedNames[0] =
        "Changed Copy";

    Console.WriteLine(
        "Original array:");

    foreach (string name in names)
    {
        Console.WriteLine(name);
    }

    Console.WriteLine(
        "Copied array:");

    foreach (string name in copiedNames)
    {
        Console.WriteLine(name);
    }
}


// ======================================================
// Part 5 — Duration Analysis
// ======================================================

static int GetTotalDuration(
    int[] durations)
{
    int total = 0;

    foreach (int duration in durations)
    {
        total += duration;
    }

    return total;
}


static double GetAverageDuration(
    int[] durations)
{
    int total =
        GetTotalDuration(durations);

    return (double)total /
           durations.Length;
}


static int GetShortestDuration(
    int[] durations)
{
    int shortest =
        durations[0];

    for (int i = 1;
         i < durations.Length;
         i++)
    {
        if (durations[i] < shortest)
        {
            shortest =
                durations[i];
        }
    }

    return shortest;
}


static int GetLongestDuration(
    int[] durations)
{
    int longest =
        durations[0];

    for (int i = 1;
         i < durations.Length;
         i++)
    {
        if (durations[i] > longest)
        {
            longest =
                durations[i];
        }
    }

    return longest;
}


static void SortDurations(
    int[] durations)
{
    int[] sortedDurations =
        new int[durations.Length];

    Array.Copy(
        durations,
        sortedDurations,
        durations.Length);

    Array.Sort(
        sortedDurations);

    Console.WriteLine(
        "Sorted durations:");

    foreach (int duration in sortedDurations)
    {
        Console.WriteLine(
            duration);
    }
}


// ======================================================
// Part 7.1 — ref
// ======================================================

static void ChangeWithRef(
    ref int value)
{
    value = 999;
}


// ======================================================
// Part 7.2 — out
// ======================================================

static bool TryGetSessionInfo(
    string[] names,
    int[] durations,
    string? searchName,
    out int index,
    out int duration)
{
    index =
        Array.IndexOf(
            names,
            searchName);

    if (index == -1)
    {
        duration = 0;

        return false;
    }

    duration =
        durations[index];

    return true;
}


// ======================================================
// Part 7.3 — Reference Type Without ref
// ======================================================

static void ModifyFirstDuration(
    int[] durations)
{
    durations[0] = 999;
}


// ======================================================
// Part 8 — params Keyword
// ======================================================

static int CalculateTotalDuration(
    params int[] durations)
{
    int total = 0;

    foreach (int duration in durations)
    {
        total += duration;
    }

    return total;
}


// ======================================================
// Part 9 — Session Date Details
// ======================================================

static void DisplaySessionDetails(
    string[] names,
    DateTime[] dates,
    int[] durations)
{
    Console.Write(
        "Enter session name for details: ");

    string? searchName =
        Console.ReadLine();

    int index =
        Array.IndexOf(
            names,
            searchName);

    if (index == -1)
    {
        Console.WriteLine(
            "Session not found.");

        return;
    }

    DateTime sessionDate =
        dates[index];

    int duration =
        durations[index];

    DateTime endTime =
        GetSessionEndTime(
            sessionDate,
            duration);

    Console.WriteLine(
        $"Session: {names[index]}");

    Console.WriteLine(
        $"Date: {sessionDate:dd MMMM yyyy}");

    Console.WriteLine(
        $"Day: {sessionDate.DayOfWeek}");

    Console.WriteLine(
        $"Year: {sessionDate.Year}");

    Console.WriteLine(
        $"Month: {sessionDate.Month}");

    Console.WriteLine(
        $"Day Number: {sessionDate.Day}");

    Console.WriteLine(
        $"Start Time: {sessionDate:hh:mm tt}");

    Console.WriteLine(
        $"Duration: {duration} minutes");

    Console.WriteLine(
        $"End Time: {endTime:hh:mm tt}");
}


static DateTime GetSessionEndTime(
    DateTime startTime,
    int duration)
{
    return startTime.AddMinutes(
        duration);
}


// ======================================================
// Part 10 — Date Difference
// ======================================================

static void CompareSessionDates(
    string[] names,
    DateTime[] dates)
{
    Console.Write(
        "First Session: ");

    string? firstName =
        Console.ReadLine();

    Console.Write(
        "Second Session: ");

    string? secondName =
        Console.ReadLine();

    int firstIndex =
        Array.IndexOf(
            names,
            firstName);

    int secondIndex =
        Array.IndexOf(
            names,
            secondName);

    if (firstIndex == -1 ||
        secondIndex == -1)
    {
        Console.WriteLine(
            "One or both sessions were not found.");

        return;
    }

    TimeSpan difference =
        dates[secondIndex] -
        dates[firstIndex];

    Console.WriteLine(
        "Difference:");

    Console.WriteLine(
        $"{difference.TotalDays} days");

    Console.WriteLine(
        $"{difference.TotalHours} hours");
}


// ======================================================
// Part 11 — Past and Upcoming Sessions
// ======================================================

static void ShowSessionStatus(
    string[] names,
    DateTime[] dates)
{
    DateTime now =
        DateTime.Now;

    for (int i = 0;
         i < names.Length;
         i++)
    {
        string status;

        if (dates[i] < now)
        {
            status = "Past";
        }
        else
        {
            status = "Upcoming";
        }

        Console.WriteLine(
            $"{names[i]} - {status}");
    }
}


// ======================================================
// Part 12 — Find the Next Session
// ======================================================

static void FindNextSession(
    string[] names,
    DateTime[] dates)
{
    DateTime now =
        DateTime.Now;

    // -1 means no upcoming candidate has been found yet.
    int nextIndex = -1;

    for (int i = 0;
         i < dates.Length;
         i++)
    {
        if (dates[i] <= now)
        {
            continue;
        }

        if (nextIndex == -1 ||
            dates[i] < dates[nextIndex])
        {
            nextIndex = i;
        }
    }

    if (nextIndex == -1)
    {
        Console.WriteLine(
            "No upcoming sessions.");

        return;
    }

    DateTime nextDate =
        dates[nextIndex];

    TimeSpan remaining =
        nextDate - now;

    Console.WriteLine(
        "Next Session:");

    Console.WriteLine(
        names[nextIndex]);

    Console.WriteLine(
        $"{nextDate:dd MMMM yyyy}");

    Console.WriteLine(
        $"{nextDate:hh:mm tt}");

    Console.WriteLine(
        "Time Remaining:");

    Console.WriteLine(
        $"{remaining.Days} days");

    Console.WriteLine(
        $"{remaining.Hours} hours");
}


// ======================================================
// Part 13 — Date Formatting
// ======================================================

static void DisplayDateFormats(
    DateTime date)
{
    Console.WriteLine(
        date.ToString(
            "yyyy-MM-dd"));

    Console.WriteLine(
        date.ToString(
            "dd/MM/yyyy"));

    Console.WriteLine(
        date.ToString(
            "dd MMMM yyyy"));

    Console.WriteLine(
        date.ToString(
            "dddd, dd MMMM yyyy"));

    Console.WriteLine(
        date.ToString(
            "hh:mm tt"));
}


// ======================================================
// Part 14 — Read and Validate a Date
// ======================================================

static DateTime ReadSessionDate()
{
    while (true)
    {
        Console.Write(
            "Enter date (yyyy-MM-dd HH:mm): ");

        string? input =
            Console.ReadLine();

        bool success =
            DateTime.TryParseExact(
                input,
                "yyyy-MM-dd HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime validDate);

        if (success)
        {
            return validDate;
        }

        Console.WriteLine(
            "Invalid date. Please try again.");
    }
}


// ======================================================
// Part 15 — Exception Handling: Menu Input
// ======================================================

static int ReadMenuOption()
{
    while (true)
    {
        Console.Write(
            "Choose an option: ");

        string? input =
            Console.ReadLine();

        try
        {
            int option =
                int.Parse(input!);

            return option;
        }
        catch (FormatException)
        {
            Console.WriteLine(
                "Invalid menu option. Enter a number.");
        }
    }
}


// ======================================================
// Part 16 — Exception Handling: Invalid Array Index
// Part 18 — finally
// ======================================================

static void ReadSessionByIndex(
    string[] names)
{
    Console.Write(
        "Enter session index: ");

    int index =
        int.Parse(
            Console.ReadLine()!);

    try
    {
        Console.WriteLine(
            $"Session: {names[index]}");
    }
    catch (IndexOutOfRangeException)
    {
        Console.WriteLine(
            "The selected session index is out of range.");
    }
    finally
    {
        Console.WriteLine(
            "Input operation finished.");
    }
}


// ======================================================
// Part 17 — Throw an Exception
// ======================================================

static void ValidateDuration(
    int duration)
{
    if (duration <= 0)
    {
        throw new ArgumentException(
            "Duration must be greater than zero.");
    }

    Console.WriteLine(
        "Duration accepted.");
}
// ======================================================
// Part 19 — Build a Schedule Report Using string
// ======================================================

static string BuildScheduleReport(
    string[] names,
    DateTime[] dates,
    int[] durations)
{
    // Start with an empty report.
    string result = "";

    // Add one session line during each iteration.
    for (int i = 0; i < names.Length; i++)
    {
        result +=
            $"{names[i]} - " +
            $"{dates[i]:dd/MM/yyyy hh:mm tt} - " +
            $"{durations[i]} minutes\n";
    }

    // Return the complete report.
    return result;
}
// ======================================================
// Part 20 — Build the Same Report Using StringBuilder
// ======================================================

static string BuildScheduleReportWithStringBuilder(
    string[] names,
    DateTime[] dates,
    int[] durations)
{
    // Create an empty mutable string builder.
    StringBuilder result = new StringBuilder();

    // Add one session line during each iteration.
    for (int i = 0; i < names.Length; i++)
    {
        result.AppendLine(
            $"{names[i]} - " +
            $"{dates[i]:dd/MM/yyyy hh:mm tt} - " +
            $"{durations[i]} minutes");
    }

    // Convert the StringBuilder to a string and return it.
    return result.ToString();
}


// ======================================================
// Part 31 — Console Menu
// ======================================================

static void DisplayMenu()
{
    Console.WriteLine();
    Console.WriteLine("===================================");
    Console.WriteLine("Academy Schedule Analyzer");
    Console.WriteLine("===================================");
    Console.WriteLine("1. Display all sessions");
    Console.WriteLine("2. Search for a session");
    Console.WriteLine("3. Sort session names");
    Console.WriteLine("4. Reverse session names");
    Console.WriteLine("5. Find session index");
    Console.WriteLine("6. Check if session exists");
    Console.WriteLine("7. Show duration statistics");
    Console.WriteLine("8. Show session date details");
    Console.WriteLine("9. Show past and upcoming sessions");
    Console.WriteLine("10. Find next session");
    Console.WriteLine("11. Compare two session dates");
    Console.WriteLine("12. Read and validate a custom date");
    Console.WriteLine("13. Select session by index");
    Console.WriteLine("14. Validate session duration");
    Console.WriteLine("15. Generate report using string");
    Console.WriteLine("16. Generate report using StringBuilder");
    Console.WriteLine("0. Exit");
    Console.WriteLine();
    Console.Write("Choose an option: ");
}


