using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Entity.Concrete;
using TaskManager.Core.Enum;

namespace TaskManager.Entities.Entities
{
    public class Tasks:CoreEntity
    {
        public Tasks()
        {
            CreatedDate = DateTime.Now;
            EndDate = DateTime.Now;
        }
        public DateTime CreatedDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public TaskSchedule TaskSchedule { get; set; }
        public State State { get; set; }
        public virtual User User { get; set; }
        
        public string userMail { get; set; }
        public string userID { get; set; }

        //
    }
}
