using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileWatcherApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileWatcherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileDetailsController : Controller
    {
        private readonly IFileDetailsAction _fileDetails;

        public FileDetailsController(IFileDetailsAction fileDetails)
        {
            this._fileDetails = fileDetails;
        }

        //API-GET call to get all records in task name table
        [HttpGet("taskName")]
        public async Task<ActionResult<List<FileDetails>>> GetAllTaskName()
        {
            try
            {
                var fileDetails = await _fileDetails.GetTaskName();
                return Ok(fileDetails);
            }
            catch (Exception)
            {
                string errorString = "Error in retrieving task details from database.";
                await CaptureLogDetails(errorString);
                return StatusCode(StatusCodes.Status500InternalServerError, errorString);
            }
        }

        //API-GET call to get all records in task status table
        [HttpGet("taskStatus")]
        public async Task<ActionResult<List<FileDetails>>> GetAllTaskStatus()
        {
            try
            {
                var fileDetails = await _fileDetails.GetTaskStatus();
                return Ok(fileDetails);
            }
            catch (Exception)
            {
                string errorString = "Error in retrieving task details from database.";
                await CaptureLogDetails(errorString);
                return StatusCode(StatusCodes.Status500InternalServerError, errorString);
            }
        }

        //API-GET call to get all records in Change Type table
        [HttpGet("changeType")]
        public async Task<ActionResult<List<FileDetails>>> GetAllChangeType()
        {
            try
            {
                var fileDetails = await _fileDetails.GetChangeType();
                return Ok(fileDetails);
            }
            catch (Exception)
            {
                string errorString = "Error in retrieving task details from database.";
                await CaptureLogDetails(errorString);
                return StatusCode(StatusCodes.Status500InternalServerError, errorString);
            }
        }

        //API-GET call to get all records in Task Details table
        [HttpGet("taskDetails")]
        public async Task<ActionResult<List<FileDetails>>> GetAllTaskDetails()
        {
            try
            {
                var fileDetails = await _fileDetails.GetTaskDetails();
                return Ok(fileDetails);
            }
            catch (Exception)
            {
                string errorString = "Error in retrieving task details from database.";
                await CaptureLogDetails(errorString);
                return StatusCode(StatusCodes.Status500InternalServerError, errorString);
            }
        }

        //API-GET call to get all records in file details table
        [HttpGet("fileDetails")]
        public async Task<ActionResult<List<FileDetails>>> GetAllFileDetails()
        {
            try
            {
                var fileDetails = await _fileDetails.GetFileDetails();
                return Ok(fileDetails);
            }
            catch(Exception)
            {
                string errorString = "Error in retrieving task details from database.";
                await CaptureLogDetails(errorString);
                return StatusCode(StatusCodes.Status500InternalServerError, errorString);
            }
        }

        //API-GET call to get all records in log table
        [HttpGet("logging")]
        public async Task<ActionResult<List<FileDetails>>> GetAllLogging()
        {
            try
            {
                var logging = await _fileDetails.GetLogging();
                return Ok(logging);
            }
            catch (Exception)
            {
                string errorString = "Error in retrieving log details from database.";
                await CaptureLogDetails(errorString);
                return StatusCode(StatusCodes.Status500InternalServerError, errorString);
            }
        }

        //API-PUT call to update an existing row to Task Details
        [HttpPut("{id}")]
        public async Task<ActionResult<TaskDetails>> UpdateTaskDetails(int id, TaskDetails taskDetails)
        {
            try
            {
                var putResult = await _fileDetails.UpdateTaskDetails(id, taskDetails);
                return Ok(putResult);
            }
            catch (Exception)
            {
                string errorString = "Error in updating task details from database.";
                await CaptureLogDetails(errorString);
                return StatusCode(StatusCodes.Status500InternalServerError, errorString);
            }
        }

        //API-POST call to insert new row to Task Details
        [HttpPost("insertNewTaskDetails")]
        public async Task<ActionResult<TaskDetails>> AddNewFileDetails(TaskDetails taskDetails)
        {
            try
            {
                if (taskDetails == null)
                {
                    string errorString = "Error, imput details are empty.";
                    await CaptureLogDetails(errorString);
                    return StatusCode(StatusCodes.Status500InternalServerError, errorString);
                }

                string taskStatus = await _fileDetails.CheckLastTaskStatus(taskDetails);
                if (taskStatus != null)
                {
                    string errorString = taskStatus;
                    await CaptureLogDetails(errorString);
                    return StatusCode(StatusCodes.Status500InternalServerError, taskStatus);
                }

                if (taskDetails.TaskStatusId == 1)
                {
                    string errorString = null;
                    errorString = _fileDetails.Start();
                    if (errorString == null)
                    {
                        errorString = $"New task started. task ID={taskDetails.TaskDetailsId}";
                    }
                    await CaptureLogDetails(errorString);
                    return StatusCode(StatusCodes.Status500InternalServerError, errorString);
                }
                else if (taskDetails.TaskStatusId == 2)
                {
                    _fileDetails.Stop();
                    string errorString = $"Task stoped. task ID={taskDetails.TaskDetailsId}";
                    await CaptureLogDetails(errorString);
                }

                var newFileTasks = await _fileDetails.AddNewTaskDetails(taskDetails);
                if (newFileTasks == null)
                {
                    string errorString = "Error, Some of the foreign key refferance are missing.";
                    await CaptureLogDetails(errorString);
                    return StatusCode(StatusCodes.Status500InternalServerError, errorString);
                }
                return Ok(newFileTasks);
            }
            catch (Exception)
            {
                string errorString = "Error in creating new task details into database.";
                await CaptureLogDetails(errorString);
                return StatusCode(StatusCodes.Status500InternalServerError, errorString);
            }
        }

        //Used to capture log details
        private async Task CaptureLogDetails(string logDetails)
        {
            await _fileDetails.AddLogDetails(logDetails);
        }
        
    }
}


