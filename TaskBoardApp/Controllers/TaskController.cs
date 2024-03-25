namespace TaskBoardApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Runtime.CompilerServices;
    using TaskBoardApp.Extensions;
    using TaskBoardApp.Services.Interfaces;
    using TaskBoardApp.ViewModels.Task;

    [Authorize]
    public class TaskController : Controller
    {
        private readonly IBoardService boardService;
        private readonly ITaskService taskService;

        public TaskController(IBoardService boardService, ITaskService taskService)
        {
            this.boardService = boardService;
            this.taskService = taskService;

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TaskFormModel model = new TaskFormModel()
            {
                Boards = await this.boardService.AllForSelectAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Boards = await this.boardService.AllForSelectAsync();
                return this.View(model);
            }

            bool boardExists = await this.boardService.ExistsByIdAsync(model.BoardId);

            if (!boardExists)
            {
                ModelState.AddModelError(nameof(model.BoardId), "Selected board does not exist!");
                model.Boards = await this.boardService.AllForSelectAsync();
                return View(model);
            }

            string currentUserId = this.User.GetId();

            await taskService.AddAsync(currentUserId, model);

            return RedirectToAction("All", "Board");
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)

        {
            try
            {
                TaskDetailsViewModel viewModel = await this.taskService.GetForDetailsByIdAsync(id);

                return View(viewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("All", "Board");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var task = await this.taskService.GetTaskByIdAsync(id);

            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = this.User.GetId();
            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            TaskFormModel model = new TaskFormModel()
            {
                Title = task.Title,
                Description = task.Description,
                BoardId = task.BoardId,
                Boards = await this.boardService.AllForSelectAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, TaskFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Boards = await this.boardService.AllForSelectAsync();
                return this.View(model);
            }

            bool boardExists = await this.boardService.ExistsByIdAsync(model.BoardId);

            if (!boardExists)
            {
                ModelState.AddModelError(nameof(model.BoardId), "Selected board does not exist!");
                model.Boards = await this.boardService.AllForSelectAsync();
                return View(model);
            }

            await taskService.EditAsync(id, model);

            return RedirectToAction("All", "Board");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var task = await this.taskService.GetTaskByIdAsync(id);

            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = this.User.GetId();
            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            TaskViewModel model = new TaskViewModel()
            {
                Id = task.Id.ToString(),
                Title = task.Title,
                Description = task.Description,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TaskViewModel model)
        {
            await taskService.DeleteAsync(model);

            return RedirectToAction("All", "Board");
        }
    }
}
