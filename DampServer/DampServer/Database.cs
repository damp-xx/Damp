#region

using System;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace DampServer
{
    internal class Database
    {
        private const string ConnectionString =
            "data source=localhost\\SQLEXPRESS;initial catalog=Damp;user id=root;password=mormor";

        private readonly SqlConnection _sqlConnection = new SqlConnection(ConnectionString);

        public bool Open()
        {
            if (_sqlConnection.State == ConnectionState.Open) return true;

            try
            {
                _sqlConnection.Open();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Database Exception 1: {0}", e.Message);
                return false;
            }

            return true;
        }


        public SqlCommand GetCommand()
        {
            return new SqlCommand {Connection = _sqlConnection, CommandType = CommandType.Text};
        }


        public void Close()
        {
            _sqlConnection.Close();
        }
    }
}