using System;
using System.Collections.Generic;
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

        // to do: functii de genul getTskList
        //exemplu mai jos:
        public async Task getTaskList()
        {
            // scris cod, folosint connection string-ul de mai sus,, pt a luat lista task-urile din db
        }

    }
}
