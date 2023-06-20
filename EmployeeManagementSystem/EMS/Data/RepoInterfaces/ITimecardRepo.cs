using EMS.Data.Models;

namespace EMS.Data.RepoInterfaces;

public interface ITimecardRepo
{
	Task<List<Timecard>> GetAllOfUserAsync(string appUserId);
	Task<List<Timecard>> GetAllUnapprovedOfUserAsync(string appUserId);
  Task<Timecard> GetByIdAsync(int id);
	bool Add(Timecard timecard);
	bool Update(Timecard timecard);
	bool Delete(Timecard timecard);
	bool Save();
}
