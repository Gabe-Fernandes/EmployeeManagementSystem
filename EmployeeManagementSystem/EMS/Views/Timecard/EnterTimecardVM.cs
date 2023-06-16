namespace EMS.Views.Timecard;

public class EnterTimecardVM
{
  public bool CardSubmitted { get; set; }
    
  public bool IsApproved { get; set; }

  public int TimecardId { get; set; }

  public string AppUserId { get; set; }

  public int WeeklyHours { get; set; }

  public string Status { get; set; }

  public static string ConvertValueToTimeFormat(double timeValue)
  {
     string meridiemState = (timeValue >= 12) ? "PM" : "AM";
     if (timeValue >= 13) { timeValue -= 12; } // switch to 12 hour time
     string timeValueString = timeValue.ToString(); // allows us to reference chars
     string convertedTime = $"{ConvertedHours(timeValueString)}:{ConvertedMinutes(timeValueString, 2)} {meridiemState}";
     return convertedTime;
  }

  private static string ConvertedHours(string timeValue)
  {
    // if the hours have 1 digit
    if (timeValue.Length == 1 || timeValue[1] == '.')
    {
      return $"{timeValue[0]}";
    }
    // if the hours have 2 digits
    else
    {
      return $"{timeValue[0]}{timeValue[1]}";
    }
  }

  private static string ConvertedMinutes(string timeValue, int index)
  {
    if (timeValue.Length < 3) { return "00"; } // x.0

    if (timeValue[index - 1] == '.')
    {
    switch (timeValue[index])
    {
      case '2':
        return "15"; // x.25
      case '5':
        return "30"; // x.5
      case '7':
        return "45"; // x.75
    }
    }
    else
    {
    return ConvertedMinutes(timeValue, 3);
    }
    return "";
    }
}
