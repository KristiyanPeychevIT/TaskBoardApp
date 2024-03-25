namespace TaskBoardApp.Data.Configurations
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Task = Models.Task;

    internal class TaskEntityConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder
                .HasOne(t => t.Board)
                .WithMany(b => b.Tasks)
                .HasForeignKey(t => t.BoardId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasData(this.GenerateTasks());
        }

        private ICollection<Task> GenerateTasks()
        {
            ICollection<Task> tasks = new HashSet<Task>()
            {
                new Task
                {
                    Title = "Improve CSS styles",
                    Description = "Implement better styling for all public pages",
                    CreatedOn = DateTime.UtcNow.AddDays(-200),
                    OwnerId = "8ed158dd-4cdc-4065-ab4d-7923e877e0d0",
                    BoardId = 1
                },
                new Task
                {
                    Title = "Android Client App",
                    Description = "Create Android client App for the RESTful TaskBoard service",
                    CreatedOn = DateTime.UtcNow.AddMonths(-5),
                    OwnerId = "0051562c-226e-48bb-a055-20712b30a33e",
                    BoardId = 1
                },
                new Task
                {
                    Title = "Desktop Client App",
                    Description = "Create Desktop client App for the RESTful TaskBoard service",
                    CreatedOn = DateTime.UtcNow.AddMonths(-1),
                    OwnerId = "8ed158dd-4cdc-4065-ab4d-7923e877e0d0",
                    BoardId = 2
                },
                new Task
                {
                    Title = "Create Tasks",
                    Description = "Implement [Create Task] page for adding tasks",
                    CreatedOn = DateTime.UtcNow.AddYears(-1),
                    OwnerId = "8ed158dd-4cdc-4065-ab4d-7923e877e0d0",
                    BoardId = 3
                }
            };

            return tasks;
        }
    }
}
