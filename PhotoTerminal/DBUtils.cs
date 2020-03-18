using MySql.Data.MySqlClient;
using System;

namespace PhotoTerminal
{
    class DBUtils
    {
        public DBUtils()
        {
        }

        public MySqlConnection getDBConnection()
        {
            String connString = @"Server=83.220.174.171;Database=CRM;user id=CRM_user;password=Az123456!;charset=utf8";
            return new MySqlConnection(connString);
        }
    }
}
