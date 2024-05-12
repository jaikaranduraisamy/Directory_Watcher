using System;

namespace FileWatcherApp.Models
{
    public class ChangeType
    {
        public int ChangeTypeId { get; set; }
        public string ChangeTypeName { get; set; }
    }

    public class TaskName
    {
        public int TaskNameId { get; set; }
        public string Name { get; set; }
    }

    public class TaskStatus
    {
        public int TaskStatusId { get; set; }
        public string StatusName { get; set; }
    }

    public class TaskDetails
    {
        public int TaskDetailsId { get; set; }
        public int TaskNameId { get; set; }
        public int TaskStatusId { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public class FileDetails
    {
        public int FileDetailsId { get; set; }
        public string FileName { get; set; }
        public DateTime TimeStamp { get; set; }

        public int TaskDetailsId { get; set; }
        public int ChangeTypeId { get; set; }
    }

    public class Logging
    {
        public int LoggingId { get; set; }
        public string LogDetails { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
