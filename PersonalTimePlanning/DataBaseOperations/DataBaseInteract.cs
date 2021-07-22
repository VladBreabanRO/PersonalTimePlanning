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

        public  Task SaveData<T>(string sql, T parameters)
        {
            using(IDbConnection connection = new SqlConnection(this._connectionString))
            {
                return connection.ExecuteAsync(sql, parameters);
            }
        }
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

        public async Task<List<toDoList>> getAllToDosList()
        {
            string sqlCommand = "select * from dbo.toDoList";
            return this.LoadData<toDoList, dynamic>(sqlCommand, new { }).GetAwaiter().GetResult();
        }
    }
}
