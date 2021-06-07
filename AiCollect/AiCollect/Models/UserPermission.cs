using System;
using System.Collections.Generic;
using System.Text;

namespace AiCollect.Models
{
    public class UserPermission : DataCollectionObject
    {
        private UserRight _userRight;
        private PermisionType _permission;
        private UserPermission _original;
    }
}
