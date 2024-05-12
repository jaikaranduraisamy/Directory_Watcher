using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FileWatcherApp.Models
{
    public interface IFileDetailsAction
    {
        //GET methods
        Task<IEnumerable<TaskName>> GetTaskName();
        Task<IEnumerable<TaskStatus>> GetTaskStatus();
        Task<IEnumerable<TaskDetails>> GetTaskDetails();
        Task<IEnumerable<ChangeType>> GetChangeType();
        Task<IEnumerable<Logging>> GetLogging();
        Task<IEnumerable<FileDetails>> GetFileDetails();

        //PUT methods
        Task<TaskDetails> UpdateTaskDetails(int id, TaskDetails taskDetails);

        //Method to insert new task
        /*Task<FileDetails> AddNewTask(FileDetails fileDetails);*/
        Task<TaskDetails> AddNewTaskDetails(TaskDetails taskDetails);
        Task AddLogDetails(string logDetails);


        Task<string> CheckLastTaskStatus(TaskDetails taskDetails);

        string Start();
        void Stop();
        
    }
}
