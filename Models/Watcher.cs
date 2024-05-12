using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace FileWatcherApp.Models
{
    public class Watcher
    {
        private string _watchPath;
        private FileSystemWatcher _watcher;
        private SqliteConnection connection;
        private string connectionString;

        public Watcher()
        {
            string path_location = File.ReadAllText(@"PathLocation.txt");
            _watchPath = path_location;
            _watcher = new FileSystemWatcher();

            connectionString = $"Data Source=FileDetail.db;";
            connection = new SqliteConnection(connectionString);
        }

        public string Start()
        {
            try
            {
                _watcher.Path = _watchPath;
                _watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;
                _watcher.Filter = "*.*";
                _watcher.Created += OnCreated;
                _watcher.Deleted += OnDeleted;
                _watcher.EnableRaisingEvents = true;
                return null;
            }
            catch (Exception)
            {
                return "Wrong Path Location";
            }
        }

        public void Stop()
        {
            _watcher.Dispose();
        }


        public void OpenConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
            }
            catch (Exception)
            {
                AddLog("Error in opening database connection in directory watcher");
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception)
            {
                AddLog("Error in closing database connection in directory watcher");
            }
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            int taskDetailId = GetLastTaskId();
            try
            {
                OpenConnection();
                using (SqliteCommand insertData = new SqliteCommand($"INSERT INTO fileDetails (FileName,TaskDetailsId,ChangeTypeId,TimeStamp) VALUES ('{e.Name}',{taskDetailId},1,'{DateTime.Now}');", connection))
                {
                    insertData.ExecuteNonQuery();
                    AddLog($"{e.Name}, File Added to the the path location {e.FullPath}");
                }
                CloseConnection();
            }
            catch(Exception)
            {
                AddLog("Error in inserting file details when new file created");
            }
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            int taskDetailId = GetLastTaskId();
            try
            {
                OpenConnection();
                using (SqliteCommand insertData = new SqliteCommand($"INSERT INTO fileDetails (FileName,TaskDetailsId,ChangeTypeId,TimeStamp) VALUES ('{e.Name}',{taskDetailId},2,'{DateTime.Now}');", connection))
                {
                    insertData.ExecuteNonQuery();
                    AddLog($"{e.Name}, File Deleted from the the path location {e.FullPath}");
                }
                CloseConnection();
            }
            catch (Exception)
            {
                AddLog("Error in deleting file details when new file created");
            }
        }

        private int GetLastTaskId()
        {
            try
            {
                int taskDetailsId = -1;
                OpenConnection();

                using (SqliteCommand selectData = new SqliteCommand("SELECT TaskDetailsId FROM taskDetails order by TaskDetailsId desc limit 1;", connection))
                {
                    using (SqliteDataReader reader = selectData.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                             taskDetailsId = Convert.ToInt32(reader["TaskDetailsId"]);
                        }
                    }
                }
                return taskDetailsId;
            }
            catch(Exception)
            {
                AddLog("Error in getting last running/closed task Id");
                return -1;
            }
                            
        }


        private void AddLog(string logString)
        {
            try
            {
                OpenConnection();
                using (SqliteCommand insertData = new SqliteCommand($"INSERT INTO logging (LogDetails,TimeStamp) VALUES ('{logString}','{DateTime.Now}');", connection))
                {
                    insertData.ExecuteNonQuery();
                }
                CloseConnection();
            }
            catch (Exception)
            {
                
            }
        }
    }
}
