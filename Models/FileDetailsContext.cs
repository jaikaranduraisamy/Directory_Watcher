using Microsoft.EntityFrameworkCore;

namespace FileWatcherApp.Models
{
    public class FileDetailsContext : DbContext
    {
        public FileDetailsContext(DbContextOptions<FileDetailsContext> options) : base(options)
        {
        }

        public DbSet<ChangeType> changeTypes { get; set; }
        public DbSet<TaskName> taskName { get; set; }
        public DbSet<TaskStatus> taskStatus { get; set; }
        public DbSet<TaskDetails> taskDetails { get; set; }
        public DbSet<FileDetails> fileDetails { get; set; }
        public DbSet<Logging> logging { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Master data for the ChangeType table
            modelBuilder.Entity<ChangeType>().Property<int>("ChangeTypeId").IsRequired();
            ChangeType changeType = new ChangeType
            {
                ChangeTypeId = 1,
                ChangeTypeName = "File Added"
            };
            modelBuilder.Entity<ChangeType>().HasData(changeType);

            changeType = new ChangeType
            {
                ChangeTypeId = 2,
                ChangeTypeName = "File Deleted"
            };
            modelBuilder.Entity<ChangeType>().HasData(changeType);


            //Master data for the TaskName table
            modelBuilder.Entity<TaskName>().Property<int>("TaskNameId").IsRequired();
            TaskName taskName = new TaskName
            {
                TaskNameId = 1,
                Name = "Task Started"
            };
            modelBuilder.Entity<TaskName>().HasData(taskName);

            taskName = new TaskName
            {
                TaskNameId = 2,
                Name = "Task Ended"
            };
            modelBuilder.Entity<TaskName>().HasData(taskName);

            //Master data for the TaskStatus table
            modelBuilder.Entity<TaskStatus>().Property<int>("TaskStatusId").IsRequired();
            TaskStatus taskStatus = new TaskStatus
            {
                TaskStatusId = 1,
                StatusName = "Running"
            };
            modelBuilder.Entity<TaskStatus>().HasData(taskStatus);

            taskStatus = new TaskStatus
            {
                TaskStatusId = 2,
                StatusName = "Ended"
            };
            modelBuilder.Entity<TaskStatus>().HasData(taskStatus);

            //Master data for the TaskDetails table
            modelBuilder.Entity<TaskDetails>().Property<int>("TaskDetailsId").IsRequired();

            //Master data for the FileDetails table
            modelBuilder.Entity<FileDetails>().Property<int>("FileDetailsId").IsRequired();

            //Master data for the Logging table
            modelBuilder.Entity<Logging>().Property<int>("LoggingId").IsRequired();
        }
    }
}
