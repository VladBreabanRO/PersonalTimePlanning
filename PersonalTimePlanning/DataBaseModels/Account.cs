using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalTimePlanning.DataBaseModels
{
    class Account
    {
        public int account_Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
        public bool? AllowNotification
        {
            get;set;
        }
        
        public string ProfilePicturePath
        {
            get;
            set;
        }

        public DateTime? StartWorkingHour
        {
            get;
            set;
        }

        public DateTime? EndWorkingHour
        {
            get;
            set;
        }

        public int? Calendar_ID
        {
            get;
            set;
        }
    }
}
