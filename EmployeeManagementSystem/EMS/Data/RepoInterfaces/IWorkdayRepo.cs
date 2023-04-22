using EMS.Data.Models;

namespace EMS.Data.RepoInterfaces;

public interface IWorkdayRepo
{
	Task<List<Workday>> GetAllFromTimecardAsync(int timecardId);
	Task<Workday> GetByIdAsync(int id);
	bool Add(Workday workday);
	bool Update(Workday workday);
	bool Delete(Workday workday);
	bool Save();
}
