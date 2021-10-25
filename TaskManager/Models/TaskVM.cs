using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Entities.Entities;

namespace TaskManager.WebUI.Models
{
    public class TaskVM
    {
        public User user { get; set; }
        public Tasks  task { get; set; }
        public LoginVM LoginVM { get; set; }
    }
}
