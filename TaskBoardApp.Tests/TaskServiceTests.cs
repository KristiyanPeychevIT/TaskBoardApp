using Microsoft.AspNetCore.Identity;

namespace TaskBoardApp.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TaskBoardApp.Data;
    using TaskBoardApp.Services;
    using TaskBoardApp.ViewModels.Task;

    [TestFixture]
    public class TaskServiceTests
    {
        private TaskBoardDbContext _dbContext;
        private TaskService _taskService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<TaskBoardDbContext>()
                .UseInMemoryDatabase(databaseName: "TaskBoardDb")
                .Options;

            _dbContext = new TaskBoardDbContext(options);
            _taskService = new TaskService(_dbContext);

            // Seed data
            var owner = new IdentityUser() { UserName = "testUser", Id = "user-123" };
            _dbContext.Users.Add(owner);
            _dbContext.Tasks.Add(new Data.Models.Task
            {
                Id = Guid.NewGuid(),
                Title = "Test Task 1",
                Description = "Task 1 Description",
                BoardId = 1,
                OwnerId = owner.Id,
                CreatedOn = DateTime.UtcNow
            });
            _dbContext.SaveChanges();
        }

        [Test]
        public async Task GetTaskByIdAsync_ReturnsCorrectTask()
        {
            var taskId = _dbContext.Tasks.First().Id.ToString();

            var task = await _taskService.GetTaskByIdAsync(taskId);

            Assert.That(task, Is.Not.Null);
            Assert.That(task.Id.ToString(), Is.EqualTo(taskId));
        }

        [Test]
        public async Task EditAsync_UpdatesTaskDetails()
        {
            var task = _dbContext.Tasks.First();
            var editedViewModel = new TaskFormModel
            {
                Title = "Updated Title",
                Description = "Updated Description",
                BoardId = 2  // Assuming board 2 exists or is irrelevant for this particular test
            };

            await _taskService.EditAsync(task.Id.ToString(), editedViewModel);

            // Reload the task from the database to verify changes
            var updatedTask = await _dbContext.Tasks.FindAsync(task.Id);

            Assert.That(updatedTask.Title, Is.EqualTo(editedViewModel.Title));
            Assert.That(updatedTask.Description, Is.EqualTo(editedViewModel.Description));
            Assert.That(updatedTask.BoardId, Is.EqualTo(editedViewModel.BoardId));
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
        }
    }
}
