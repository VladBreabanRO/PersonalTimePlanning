using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalTimePlanning.DataBaseModels
{
    class Tasks
    {
        public int task_id
        {
            get;
            set;
        }

        public string task_Name
        {
            get;
            set;
        }

        public string date
        {
            get;
            set;
        }

        public float? estimated_duration
        {
            get;
            set;
        }
        
        public DateTime? start_hour
        {
            get;
            set;
        }

        public DateTime? end_hour
        {
            get;
            set;
        }

        public int day_id
        {
            get;
            set;
        }

        public bool? done
        {
            get;
            set;
        }

        public int to_do_id
        {
            get;
            set;
        }
    }
}
