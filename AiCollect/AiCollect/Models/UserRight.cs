using System;
using System.Collections.Generic;
using System.Text;

namespace AiCollect.Models
{
    public class UserRight : DataCollectionObject
    {
        public string ObjectName { get; set; }
        public ObjectType ObjectType { get; set; }
        public string PrimaryKey { get; set; }
        public Configuration Configuration { get; set; }
        public List<UserPermission> UserPermissions { get; set; }
    }
}
