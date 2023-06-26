using EMS.Data.RepoInterfaces;
using FakeItEasy;

namespace EMS.Tests.Controllers;

public class TiemcardControllerTests
{
  private readonly IAppUserRepo _appUserRepo;
  private readonly ITimecardRepo _timecardRepo;

  public TiemcardControllerTests()
  {
    _timecardRepo = A.Fake<ITimecardRepo>();
    _appUserRepo = A.Fake<IAppUserRepo>();
  }
}
