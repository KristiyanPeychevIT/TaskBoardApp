namespace TaskBoardApp.Services
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using TaskBoardApp.Data;
    using TaskBoardApp.Services.Interfaces;
    using TaskBoardApp.ViewModels.Task;
    public class TaskService : ITaskService
    {
        private readonly TaskBoardDbContext dbContext;

        public TaskService(TaskBoardDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async System.Threading.Tasks.Task AddAsync(string ownerId, TaskFormModel viewModel)
        {
            Data.Models.Task task = new Data.Models.Task()
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                BoardId = viewModel.BoardId,
                CreatedOn = DateTime.UtcNow,
                OwnerId = ownerId
            };

            await this.dbContext.AddAsync(task);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<Data.Models.Task> GetTaskByIdAsync(string id)
        {
            Data.Models.Task task = await this.dbContext
                .Tasks.FirstAsync(t => t.Id.ToString() == id);

            return task;
        }

        public async System.Threading.Tasks.Task EditAsync(string id, TaskFormModel editedViewModel)
        {
            Data.Models.Task taskToEdit = await this.dbContext
                .Tasks
                .FirstAsync(t => t.Id.ToString() == id);

            taskToEdit.Title = editedViewModel.Title;
            taskToEdit.Description = editedViewModel.Description;
            taskToEdit.BoardId = editedViewModel.BoardId;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<TaskDetailsViewModel> GetForDetailsByIdAsync(string id)
        {
            TaskDetailsViewModel viewModel = await this.dbContext
                .Tasks
                .Select(t => new TaskDetailsViewModel
                {
                    Id = t.Id.ToString(),
                    Title = t.Title,
                    Description = t.Description,
                    Owner = t.Owner.UserName,
                    CreatedOn = t.CreatedOn.ToString("f"),
                    Board = t.Board.Name
                })
                .FirstAsync(t => t.Id == id);

            return viewModel;
        }

        public async System.Threading.Tasks.Task DeleteAsync(TaskViewModel viewModel)
        {
            Data.Models.Task taskToDelete = await this.dbContext
                .Tasks
                .FirstAsync(t => t.Id.ToString() == viewModel.Id);
            
            this.dbContext.Tasks.Remove(taskToDelete);

            await this.dbContext.SaveChangesAsync();
        }
    }
}
