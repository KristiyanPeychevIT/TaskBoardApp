namespace TaskBoardApp.ViewModels.Task
{
    using System.ComponentModel.DataAnnotations;

    using TaskBoardApp.ViewModels.Board;
    using static Common.EntityValidationConstants.Task;
    public class TaskFormModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = "Title should be at least {2} characters long.")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = "Description should be at least {2} characters long.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Board")]
        public int BoardId { get; set; }

        public IEnumerable<BoardSelectViewModel>? Boards { get; set; }
    }
}
