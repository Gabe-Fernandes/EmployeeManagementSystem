using System.ComponentModel.DataAnnotations;

namespace EMS.Data.Models;

public class Timecard
{
  [Key]
  public int Id { get; set; }

  // Submitted, Rejected, Approved, Incomplete
  public string Status { get; set; }

	public DateTime StartDate { get; set; }

  public float TimeInMon { get; set; }
  public float TimeInTues { get; set; }
  public float TimeInWed { get; set; }
  public float TimeInThur { get; set; }
  public float TimeInFri { get; set; }
  public float TimeInSat { get; set; }
  public float TimeInSun { get; set; }

  public float TimeOutMon { get; set; }
  public float TimeOutTues { get; set; }
  public float TimeOutWed { get; set; }
  public float TimeOutThur { get; set; }
  public float TimeOutFri { get; set; }
  public float TimeOutSat { get; set; }
  public float TimeOutSun { get; set; }

  public float WeeklyHours { get; set; }

  public string AppUserId { get; set; }
}
