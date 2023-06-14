using EMS.Data.Models;

namespace EMS.Data.RepoInterfaces;

public interface IAppUserRepo
{
	Task<IEnumerable<AppUser>> GetAllWithSearchFilterAsync(string filter);
	Task<AppUser> GetByIdAsync(string id);
	bool Add(AppUser appUser);
	bool Update(AppUser appUser);
	bool Delete(AppUser appUser);
	bool Save();
}
