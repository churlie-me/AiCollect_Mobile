using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AiCollect.Services
{
    public interface IDBInterface
    {
        SQLiteConnection CreateConnection();
    }
}
