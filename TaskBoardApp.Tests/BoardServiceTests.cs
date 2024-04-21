using Moq;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TaskBoardApp.Data;
using TaskBoardApp.Services;
using TaskBoardApp.ViewModels.Board;
using TaskBoardApp.Data.Models;
using Task = TaskBoardApp.Data.Models.Task;

[TestFixture]
public class BoardServiceTests
{
    private TaskBoardDbContext _dbContext;
    private BoardService _boardService;

    [SetUp]
    public void Setup()
    {
        // Set up the DbContext with InMemory
        var options = new DbContextOptionsBuilder<TaskBoardDbContext>()
            .UseInMemoryDatabase(databaseName: "TaskBoardDb")
            .Options;

        _dbContext = new TaskBoardDbContext(options);

        // Initialize the service with the DbContext
        _boardService = new BoardService(_dbContext);

        // Seed some data for testing
        _dbContext.Boards.Add(new Board { Name = "Board 1", Tasks = new List<Task>() });
        _dbContext.Boards.Add(new Board { Name = "Board 2", Tasks = new List<Task>() });
        _dbContext.SaveChanges();
    }

    [Test]
    public async System.Threading.Tasks.Task AllAsync_ReturnsCorrectTypeAndCount()
    {
        // Act
        var result = await _boardService.AllAsync();
        var resultList = result.ToList(); // Ensure it's a list for counting

        // Assert
        Assert.That(result, Is.InstanceOf<IEnumerable<BoardAllViewModel>>());
        Assert.That(resultList.Count, Is.EqualTo(2));
    }

    [Test]
    public async System.Threading.Tasks.Task ExistsByIdAsync_ReturnsTrueForExistingId()
    {
        // Arrange & Act
        var exists = await _boardService.ExistsByIdAsync(1);  // Assuming '1' is a valid ID after seeding

        // Assert

        Assert.That(exists, Is.True);
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Database.EnsureDeleted();  // Clean up the InMemory database
    }
}