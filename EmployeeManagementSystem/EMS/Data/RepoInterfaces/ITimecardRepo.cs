﻿using EMS.Data.Models;

namespace EMS.Data.RepoInterfaces;

public interface ITimecardRepo
{
	Task<List<Timecard>> GetAllAsync();
	Task<Timecard> GetByIdAsync(int id);
	bool Add(Timecard timecard);
	bool Update(Timecard timecard);
	bool Delete(Timecard timecard);
	bool Save();
}
