using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AiCollect.Models
{
    public class SQLiteQuestionaire
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string sqliteQuestionaire { get; set; }
    }
}
