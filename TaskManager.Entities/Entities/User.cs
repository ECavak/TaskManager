using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TaskManager.Core.Entity.Concrete;

namespace TaskManager.Entities.Entities
{
    [CollectionName("User")]
    public class User:MongoIdentityUser
    {
        public User()
        {
            CreatedDate = DateTime.Now;
            
        }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Tasks> Tasks { get; set; }
        
    }
}
