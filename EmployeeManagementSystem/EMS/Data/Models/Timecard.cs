﻿using System.ComponentModel.DataAnnotations;

namespace EMS.Data.Models;

public class Timecard
{
  [Key]
  public int Id { get; set; }

  // Submitted, Rejected, Approved, Incomplete
  public string Status { get; set; }

	public DateTime StartDate { get; set; }

	public DateTime EndDate { get; set; }

	public int WeeklyHours { get; set; }

  public string AppUserId { get; set; }
}
