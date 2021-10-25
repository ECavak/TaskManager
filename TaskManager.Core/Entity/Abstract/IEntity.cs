using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.Entity.Abstract
{
    public interface IEntity<T>
    {
        T ID { get; set; }
    }
}
