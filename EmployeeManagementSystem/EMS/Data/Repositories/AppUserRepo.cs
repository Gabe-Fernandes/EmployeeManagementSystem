﻿using EMS.Data.Models;
using EMS.Data.RepoInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EMS.Data.Repositories;

public class AppUserRepo : IAppUserRepo
{
	private readonly AppDbContext _db;

	public AppUserRepo(AppDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<AppUser>> GetAllWithSearchFilterAsync(string filter)
	{
		filter ??= string.Empty;
		filter = filter.ToUpper();

		var filteredUsers = await _db.Users.Where(u =>
			(u.FirstName + " " + u.LastName).ToUpper().Contains(filter)).ToListAsync();

		return filteredUsers;
	}

	public async Task<AppUser> GetByIdAsync(string id)
	{
		return await _db.Users.FindAsync(id);
	}

  public AppUser GetById(string id)
  {
		return _db.Users.Find(id);
  }

  public bool Add(AppUser appUser)
	{
		_db.Add(appUser);
		return Save();
	}

	public bool Update(AppUser appUser)
	{
		_db.Update(appUser);
		return Save();
	}

	public bool Delete(AppUser appUser)
	{
		_db.Remove(appUser);
		return Save();
	}

	public bool Save()
	{
		int numSaved = _db.SaveChanges(); // returns the number of entries written to the database
		return numSaved > 0;
	}
}
