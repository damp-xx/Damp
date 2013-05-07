#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace DampServer
{
    public class UserManagement
    {
        public static User GetUserByAuthToken(string authToken)
        {
            Database db = new Database();
            db.Open();

            SqlCommand cmd = db.GetCommand();

            cmd.CommandText = "SELECT TOP 1 * FROM Users WHERE authToken = @authToken";
            cmd.Parameters.Add("@authToken", SqlDbType.NVarChar).Value = authToken;
            SqlDataReader r = cmd.ExecuteReader();
            User user = null;

            if (r.HasRows)
            {
                r.Read();


                user = new User {Username = ((string) r["username"]), UserId = ((Int64) r["userid"])};
                r.Close();
                

                
            }
            else throw new UserNotFoundException();

            /*
            cmd.CommandText =
                "SELECT U.userid as userid, U.username as username FROM Users U INNER JOIN Friends F ON F.userid1 = U.userid WHERE F.userid = @userid";
            cmd.Parameters.Add("@userid", SqlDbType.BigInt).Value = user.UserId;
            r = cmd.ExecuteReader();

            user.Friends = new List<User>();

            if (r.HasRows)
            {
                while (r.Read())
                {
                    user.Friends.Add(new User {Username = (string) r["username"], UserId = (long) r["userid"]});
                }
            }
             */

            r.Close();
            db.Close();

            return user;
        }
    

        public static User GetUserById(string _id)
        {
            var id = int.Parse(_id);

            Database db = new Database();
            db.Open();

            SqlCommand cmd = db.GetCommand();

            cmd.CommandText = "SELECT TOP 1 * FROM Users WHERE userid = @id";
            cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
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