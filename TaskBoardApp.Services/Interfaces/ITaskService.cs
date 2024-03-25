namespace TaskBoardApp.Services.Interfaces
{
    using System.Threading.Tasks;
    using TaskBoardApp.ViewModels.Task;

    public interface ITaskService
    {
        Task AddAsync(string ownerId, TaskFormModel viewModel);

        Task<TaskDetailsViewModel> GetForDetailsByIdAsync(string id);

        Task<Data.Models.Task> GetTaskByIdAsync(string id);

        Task EditAsync(string id, TaskFormModel viewModel);

        Task DeleteAsync(TaskViewModel viewModel);
    }
}
