using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSinAuth.Storages
{
    public class Conexion
    {
        public IDbConnection Open()
        {
            var conn = new SQLiteConnection("Data Source=database.db;Version=3;");
            conn.Open();
            return conn;
        }
    }
}
