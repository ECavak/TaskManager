using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManager.Core.Enum
{
    public enum State
    {
        [Display(Name = "To Do")]
        ToDo =0,
        [Display(Name = "In Progress")]
        InProgress,
        [Display(Name = "Done")]
        Done,
        [Display(Name= "on hold")]
        OnHold

    }
}
