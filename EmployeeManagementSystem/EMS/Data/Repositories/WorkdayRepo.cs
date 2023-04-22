using EMS.Data.Models;
using EMS.Data.RepoInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EMS.Data.Repositories;

public class WorkdayRepo : IWorkdayRepo
{
	private readonly AppDbContext _db;

  public WorkdayRepo(AppDbContext db)
  {
		_db = db;
  }

	public async Task<List<Workday>> GetAllFromTimecardAsync(int timecardId)
	{
		return await _db.Workdays.Where(w => w.TimecardId == timecardId).ToListAsync();
	}

	public async Task<Workday> GetByIdAsync(int id)
	{
		return await _db.Workdays.FindAsync(id);
	}

	public bool Add(Workday workday)
	{
		_db.Add(workday);
		return Save();
	}

	public bool Update(Workday workday)
	{
		_db.Update(workday);
		return Save();
	}

	public bool Delete(Workday workday)
	{
		_db.Remove(workday);
		return Save();
	}

	public bool Save()
	{
		int numSaved = _db.SaveChanges(); // returns the number of entries written to the database
		return numSaved > 0;
	}
}
