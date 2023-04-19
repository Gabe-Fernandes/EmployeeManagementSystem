using System.ComponentModel.DataAnnotations;

namespace EMS.Data.Models;

public class Workday
{
	[Key]
	public int Id { get; set; }

	[Required]
	public DateTime Date { get; set; }

  public int DailyHours { get; set; }

  public string TimeIn { get; set; }

	public string TimeOut { get; set; }

	public int TimecardId { get; set; }
}
