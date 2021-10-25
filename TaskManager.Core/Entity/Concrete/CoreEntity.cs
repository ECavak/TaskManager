using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Entity.Abstract;

namespace TaskManager.Core.Entity.Concrete
{
    public class CoreEntity : IEntity<ObjectId>
    {
        [BsonId]
        public ObjectId ID { get; set; }

    }
}
