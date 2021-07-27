using Dapper;
using PersonalTimePlanning.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalTimePlanning.DataBaseOperations
{
    public class DataBaseInteract
    {
        private string _connectionString = @"Data Source = {0}\SQLEXPRESS;Initial Catalog = {1}; Integrated Security = SSPI";
        private static readonly string SERVER_NAME = Environment.MachineName;
        private static string DATABASE_NAME = "PlannerApp";

        public  DataBaseInteract()
        {
            this._connectionString = string.Format(this._connectionString, SERVER_NAME, DATABASE_NAME);
        }
        public  async Task<List<T>> LoadData<T, U>(string sql, U parameter)
        {
            using(IDbConnection connection = new SqlConnection(this._connectionString))
            {
                var rows = await connection.QueryAsync<T>(sql, parameter);
                return rows.ToList();
            }
        }

        //public  Task SaveData<T>(string sql, T parameters)
        //{
        //    using(IDbConnection connection = new SqlConnection(this._connectionString))
        //    {
        //        return connection.ExecuteAsync(sql, parameters);
        //    }
        //}
        // to do: functii de genul getTskList
        //exemplu mai jos:
        public async Task<List<Tasks>> getTaskList()
        {
            string sqlCommand = "select * from dbo.tasks";
            return this.LoadData<Tasks, dynamic>(sqlCommand,new { }).GetAwaiter().GetResult(); 
        }

        public async Task<List<Account>> getAllAccounts()
        {
            string sqlCommand = "select * from dbo.account";
            return this.LoadData<Account, dynamic>(sqlCommand, new { }).GetAwaiter().GetResult();
        }

        public async Task<List<Calendar>> getAllCalendars()
        {
            string sqlCommand = "select * from dbo.calendar";
            return this.LoadData<Calendar, dynamic>(sqlCommand, new { }).GetAwaiter().GetResult();
        }

        public async Task<List<Days>> getAllDays()
        {
            string sqlCommand = "select * from dbo.days";
            return this.LoadData<Days, dynamic>(sqlCommand, new { }).GetAwaiter().GetResult();
        }

        public async Task<List<toDoList>> getAllToDoList()
        {
            string sqlCommand = "select * from dbo.toDoList";
            return this.LoadData<toDoList, dynamic>(sqlCommand, new { }).GetAwaiter().GetResult();
        }

        public async Task<List<Calendar>> getCalendarByAccountId(int account_id)
        {
            string sqlCommand = "select * from dbo.calendar where Calendar_ID = @id";
            List<Calendar> returnedCalendar =  this.LoadData<Calendar, int>(sqlCommand, account_id).GetAwaiter().GetResult();
            return returnedCalendar;
        }

        public async Task createAccount(string Name,bool AllowNotification, string ProfilePicturePath, DateTime? StartWorkingHour, DateTime? EndWorkingHour)
        {
            string sqlCommand = "insert into dbo.account (Name,AllowNotification,ProfilePicturePath,StartWorkingHour,EndWorkingHour) values (@nam,@allNotif,@ProfilePictPath,@StartWorking,@EndWorking); SELECT SCOPE_IDENTITY()";
            SqlConnection scn = new SqlConnection();
            scn.ConnectionString = this._connectionString;
            SqlCommand scmd = new SqlCommand(sqlCommand, scn);
            //creat account
            scmd.Parameters.AddWithValue("@nam", Name);
            scmd.Parameters.AddWithValue("@allNotif", AllowNotification);
            scmd.Parameters.AddWithValue("@ProfilePictPath", ProfilePicturePath);
            scmd.Parameters.AddWithValue("@StartWorking", StartWorkingHour);
            scmd.Parameters.AddWithValue("@EndWorking", EndWorkingHour);
            scn.Open();
            //create calendar associated with account
            int insertedAccount = Convert.ToInt32(scmd.ExecuteScalarAsync().GetAwaiter().GetResult());
            string sqlCommandForCalendar = "insert into dbo.calendar (account_ID) values (@accId); SELECT SCOPE_IDENTITY()";
            scmd = new SqlCommand(sqlCommandForCalendar, scn);
            scmd.Parameters.AddWithValue("@accId", insertedAccount);
            
            int insertedCalendar = Convert.ToInt32(scmd.ExecuteScalarAsync().GetAwaiter().GetResult());

            scn.Close();

        }

        public async Task createToDoList(string name, int accountId)
        {
            string sqlCommand = "insert into dbo.toDoList (name,account_id) values (@nam,@accId); SELECT SCOPE_IDENTITY()";
            SqlConnection scn = new SqlConnection();
            scn.ConnectionString = this._connectionString;
            SqlCommand scmd = new SqlCommand(sqlCommand, scn);
            //creat account
            scmd.Parameters.AddWithValue("@nam", name);
            scmd.Parameters.AddWithValue("@accId", accountId);
            await scn.OpenAsync();
            await scmd.ExecuteNonQueryAsync();
        }

        public async Task createTask(string task_Name, string date, float estimated_duration, DateTime start_hour, DateTime end_hour, int day_id, bool done,int to_do_id)
        {
            string sqlCommand = "insert into dbo.tasks (task_Name,date,estimated_duration,start_hour,end_hour,day_id,done,to_do_id) values (@nam,@date,@est_duration,@Start,@End,@day,@done,@to_do); SELECT SCOPE_IDENTITY()";
            SqlConnection scn = new SqlConnection();
            scn.ConnectionString = this._connectionString;
            SqlCommand scmd = new SqlCommand(sqlCommand, scn);
            scmd.Parameters.AddWithValue("@nam", task_Name);
            scmd.Parameters.AddWithValue("@date", date);
            scmd.Parameters.AddWithValue("@est_duration", estimated_duration);
            scmd.Parameters.AddWithValue("@Start", start_hour);
            scmd.Parameters.AddWithValue("@End", end_hour);
            scmd.Parameters.AddWithValue("@day", day_id);
            scmd.Parameters.AddWithValue("@done", done);
            scmd.Parameters.AddWithValue("@to_do", to_do_id);
            await scn.OpenAsync();
            await scmd.ExecuteNonQueryAsync();
        }
    }
}
