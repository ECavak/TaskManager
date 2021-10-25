using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManager.Core.Enum
{
    public enum TaskSchedule
    {
        [Display(Name = "Günlük")]
        daily =0,
        [Display(Name = "Haftalık")]
        weekly,
        [Display(Name = "Aylık")]
        monthly
    }
}
