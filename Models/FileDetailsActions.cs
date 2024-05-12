using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FileWatcherApp.Models
{
    public class FileDetailsActions : IFileDetailsAction
    {
        private readonly FileDetailsContext _fileDetailsContext;
        private Watcher _watcher;

        //Constructor to initialize objects
        public FileDetailsActions(FileDetailsContext fileDetailsContext)
        {
            this._fileDetailsContext = fileDetailsContext;
            _watcher = new Watcher();
        }

        //Function to get all the records from the task name
        public async Task<IEnumerable<TaskName>> GetTaskName()
        {
            return await _fileDetailsContext.taskName.ToListAsync();
        }

        //Function to get all the records from the task status
        public async Task<IEnumerable<TaskStatus>> GetTaskStatus()
        {
            return await _fileDetailsContext.taskStatus.ToListAsync();
        }

        //Function to get all the records from the task detalils
        public async Task<IEnumerable<TaskDetails>> GetTaskDetails()
        {
            return await _fileDetailsContext.taskDetails.ToListAsync();
        }

        //Function to get all the records from the file detalils
        public async Task<IEnumerable<FileDetails>> GetFileDetails()
        {
            return await _fileDetailsContext.fileDetails.ToListAsync();
        }

        //Function to get all the records from the change types
        public async Task<IEnumerable<ChangeType>> GetChangeType()
        {
            return await _fileDetailsContext.changeTypes.ToListAsync();
        }

        //Function to get all the records from the logs
        public async Task<IEnumerable<Logging>> GetLogging()
        {
            return await _fileDetailsContext.logging.ToListAsync();
        }

        //Function to update the task details table
        public async Task<TaskDetails> UpdateTaskDetails(int id, TaskDetails taskDetails)
        {

            var entity = await _fileDetailsContext.taskDetails.FindAsync(id);

            if (entity == null)
            {
                return null;
            }

            _fileDetailsContext.Entry(taskDetails).State = EntityState.Modified;

            try
            {
                await _fileDetailsContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskDetailsExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return null;
        }

        //Function to check task details already exists or not
        private bool TaskDetailsExists(int id)
        {
            return _fileDetailsContext.taskDetails.Any(e => e.TaskDetailsId == id);
        }

        //Function to add new record to task details table
        public async Task<TaskDetails> AddNewTaskDetails(TaskDetails taskDetails)
        {
            int taskNameId = taskDetails.TaskNameId;
            int taskStatusId = taskDetails.TaskStatusId;

            if(!TaskNameExists(taskNameId))
            {
                return null;
            }

            if (!TaskStatusExists(taskStatusId))
            {
                return null;
            }

            try
            {
                var result = await _fileDetailsContext.taskDetails.AddAsync(taskDetails);
                await _fileDetailsContext.SaveChangesAsync();
                return result.Entity;
            }
            catch(Exception)
            {
                if (!TaskDetailsExists(taskDetails.TaskDetailsId))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        //Function to add logs to log table
        public async Task AddLogDetails(string logDetails)
        {
            Logging logging = new Logging
            {
                LogDetails = logDetails,
                TimeStamp = DateTime.Now
            };

            var result = await _fileDetailsContext.logging.AddAsync(logging);
            await _fileDetailsContext.SaveChangesAsync();
        }

        //Function to check task name exists
        private bool TaskNameExists(int id)
        {
            return _fileDetailsContext.taskName.Any(e => e.TaskNameId == id);
        }

        //Function to check task status exists
        private bool TaskStatusExists(int id)
        {
            return _fileDetailsContext.taskStatus.Any(e => e.TaskStatusId == id);
        }

        //Function to check last status of the task(running/closed)
        public async Task<string> CheckLastTaskStatus(TaskDetails taskDetails)
        {
            var empty = await _fileDetailsContext.taskDetails.CountAsync();
            if (empty != 0)
            {
                if (_fileDetailsContext.taskDetails.Any(e => e.TaskDetailsId == taskDetails.TaskDetailsId))
                {
                    return "Task with same Task Id is exists";
                }

                var lastRow = await _fileDetailsContext.taskDetails.OrderByDescending(e => e.TaskDetailsId).FirstAsync();
                if (lastRow.TaskStatusId == taskDetails.TaskStatusId)
                {
                    return "Already Application is Active/Shutdown. Please chaeck the application and retry later.";
                }
            }
            return null;
        }

        //Function to start directory watcher application
        public string Start()
        {
            return _watcher.Start();

        }

        //Function to stop directory watcher application
        public void Stop()
        {
            _watcher.Stop();
        }
    }
}
