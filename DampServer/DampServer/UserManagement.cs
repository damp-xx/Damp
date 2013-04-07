#region

using System;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace DampServer
{
    public class UserManagement
    {
        public static User GetUser(string authToken)
        {
            Database db = new Database();
            db.Open();

            SqlCommand cmd = db.GetCommand();

            cmd.CommandText = "SELECT TOP 1 * FROM Users WHERE authToken = @authToken";
            cmd.Parameters.Add("@authToken", SqlDbType.NVarChar).Value = authToken;
            SqlDataReader r = cmd.ExecuteReader();

            if (r.HasRows)
            {
                r.Read();


                User user = new User {Username = ((string) r["username"]), UserId = ((Int64) r["userid"])};
                r.Close();
                db.Close();

                return user;
            }

            r.Close();
            db.Close();
            throw new UserNotFoundException();
        }
    }

    public class UserNotFoundException : Exception
    {
    }
}