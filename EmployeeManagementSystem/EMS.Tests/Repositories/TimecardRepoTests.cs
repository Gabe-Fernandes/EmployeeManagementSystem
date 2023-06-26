using EMS.Data.Models;
using EMS.Data.Repositories;
using EMS.Data;
using Microsoft.EntityFrameworkCore;

namespace EMS.Tests.Repositories;
public class TimecardRepoTests
{
  private readonly AppDbContext _dbContext;
  private readonly TimecardRepo _timecardRepo;

  public TimecardRepoTests()
  {
    // Dependencies
    _dbContext = GetDbContext();
    // SUT
    _timecardRepo = new TimecardRepo(_dbContext);
  }

  private AppDbContext GetDbContext()
  {
    var options = new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
    var dbContext = new AppDbContext(options);
    dbContext.Database.EnsureCreated();
    if (dbContext.Timecards.Count() < 0)
    {
      for (int i = 0; i < 10; i++)
      {
        dbContext.Timecards.Add(new Timecard()
        {
          Id = i + 1,
          StartDate = DateTime.Now,
          AppUserId = "appUserId",
          Status = "approved",
          TimeInMon = 1,
          TimeInTues = 4,
          TimeInWed = 5,
          TimeInThur = 3,
          TimeInFri = 2,
          TimeOutMon = 1,
          TimeOutTues = 4,
          TimeOutWed = 5,
          TimeOutThur = 3,
          TimeOutFri = 2,
          WeeklyHours = 100
        });
        dbContext.SaveChangesAsync();
      }
    }
    return dbContext;
  }

  [Fact]
  public void Add_ReturnsTrue()
  {
    // Arrange
    var timecard = new Timecard()
    {
      Id = 1,
      StartDate = DateTime.Now,
      AppUserId = "appUserId",
      Status = "approved",
      TimeInMon = 1,
      TimeInTues = 4,
      TimeInWed = 5,
      TimeInThur = 3,
      TimeInFri = 2,
      TimeOutMon = 1,
      TimeOutTues = 4,
      TimeOutWed = 5,
      TimeOutThur = 3,
      TimeOutFri = 2,
      WeeklyHours = 100
    };
    // Act
    var result = _timecardRepo.Add(timecard);
    // Assert
    Assert.True(result);
  }

  [Fact]
  public void Delete_ReturnsTrue()
  {
    // Arrange
    var timecard = new Timecard()
    {
      Id = 1,
      StartDate = DateTime.Now,
      AppUserId = "appUserId",
      Status = "approved",
      TimeInMon = 1,
      TimeInTues = 4,
      TimeInWed = 5,
      TimeInThur = 3,
      TimeInFri = 2,
      TimeOutMon = 1,
      TimeOutTues = 4,
      TimeOutWed = 5,
      TimeOutThur = 3,
      TimeOutFri = 2,
      WeeklyHours = 100
    };
    _timecardRepo.Add(timecard);
    // Act
    var result = _timecardRepo.Delete(timecard);
    // Assert
    Assert.True(result);
  }

  [Fact]
  public void Update_ReturnsTrue()
  {
    // Arrange
    var timecard = new Timecard()
    {
      Id = 1,
      StartDate = DateTime.Now,
      AppUserId = "appUserId",
      Status = "approved",
      TimeInMon = 1,
      TimeInTues = 4,
      TimeInWed = 5,
      TimeInThur = 3,
      TimeInFri = 2,
      TimeOutMon = 1,
      TimeOutTues = 4,
      TimeOutWed = 5,
      TimeOutThur = 3,
      TimeOutFri = 2,
      WeeklyHours = 100
    };
    _timecardRepo.Add(timecard);
    // Act
    var result = _timecardRepo.Update(timecard);
    // Assert
    Assert.True(result);
  }

  [Fact]
  public void Save_ReturnsBool()
  {
    // Arrange (empty)
    // Act
    var result = _timecardRepo.Save();
    // Assert
    Assert.IsType<bool>(result);
  }

  [Fact]
  public async void GetByIdAsync_ReturnsAppUserTask()
  {
    // Arrange
    var id = 1;
    // Act
    var result = _timecardRepo.GetByIdAsync(id);
    // Assert
    await Assert.IsType<Task<Timecard>>(result);
  }

  [Fact]
  public async void GetAllOfUserAsync_ReturnsIEnumerableTimecardTask()
  {
    // Arrange
    string appUserId = "appUserId";
    // Act
    var result = _timecardRepo.GetAllOfUserAsync(appUserId);
    // Assert
    await Assert.IsType<Task<List<Timecard>>>(result);
  }

  [Fact]
  public async void GetAllUnapprovedOfUserAsync_ReturnsIEnumerableTimecardTask()
  {
    // Arrange
    string appUserId = "appUserId";
    // Act
    var result = _timecardRepo.GetAllUnapprovedOfUserAsync(appUserId);
    // Assert
    await Assert.IsType<Task<List<Timecard>>>(result);
  }
}
