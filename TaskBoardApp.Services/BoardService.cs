namespace TaskBoardApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TaskBoardApp.Data;
    using TaskBoardApp.Services.Interfaces;
    using TaskBoardApp.ViewModels.Board;
    using TaskBoardApp.ViewModels.Task;

    public class BoardService : IBoardService
    {
        private readonly TaskBoardDbContext dbContext;

        public BoardService(TaskBoardDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<BoardAllViewModel>> AllAsync()
        {
            IEnumerable<BoardAllViewModel> boards = await dbContext
                .Boards.Select(b => new BoardAllViewModel()
                {
                    Name = b.Name,
                    Tasks = b.Tasks
                        .Select(t => new TaskViewModel()
                        {
                            Id = t.Id.ToString(),
                            Title = t.Title,
                            Description = t.Description,
                            Owner = t.Owner.UserName
                        }).ToArray()
                }).ToArrayAsync();

            return boards;
        }

        public async Task<IEnumerable<BoardSelectViewModel>> AllForSelectAsync()
        {
            IEnumerable<BoardSelectViewModel> allBoards = await this.dbContext.Boards
                .Select(b => new BoardSelectViewModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                }).ToArrayAsync();

            return allBoards;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            bool result = await this.dbContext
                .Boards
                .AnyAsync(b => b.Id == id);

            return result;
        }
    }
}
