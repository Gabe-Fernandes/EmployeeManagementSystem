using EMS.Data.Models;
using EMS.Data.RepoInterfaces;
using EMS.Services;
using Microsoft.EntityFrameworkCore;

namespace EMS.Data.Repositories;

public class TimecardRepo : ITimecardRepo
{
	private readonly AppDbContext _db;

	public TimecardRepo(AppDbContext db)
	{
		_db = db;
	}

	public async Task<List<Timecard>> GetAllOfUserAsync(string appUserId)
	{
		var timecardsOfUser = await _db.Timecards.Where(t => t.AppUserId == appUserId).ToListAsync();
		return timecardsOfUser;
	}

  public async Task<List<Timecard>> GetAllUnapprovedOfUserAsync(string appUserId)
	{
    var unapprovedTimecardsOfUser = await _db.Timecards
			.Where(t => t.AppUserId == appUserId && t.Status != Str.Approved).ToListAsync();
    return unapprovedTimecardsOfUser;
  }

  public async Task<Timecard> GetByIdAsync(int id)
	{
		return await _db.Timecards.FindAsync(id);
	}

	public bool Add(Timecard timecard)
	{
		_db.Timecards.Add(timecard);
		return Save();
	}

	public bool Update(Timecard timecard)
	{
		_db.Timecards.Update(timecard);
		return Save();
	}

	public bool Delete(Timecard timecard)
	{
		_db.Timecards.Remove(timecard);
		return Save();
	}

	public bool Save()
	{
		int numSaved = _db.SaveChanges(); // returns the number of entries written to the database
		return numSaved > 0;
	}
}
