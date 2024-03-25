namespace TaskBoardApp.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TaskBoardApp.ViewModels.Board;

    public interface IBoardService
    {
        Task<IEnumerable<BoardAllViewModel>> AllAsync();

        Task<IEnumerable<BoardSelectViewModel>> AllForSelectAsync();

        Task<bool> ExistsByIdAsync(int id);
    }

}
