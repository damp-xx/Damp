#region

using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace Damp
{
    /**
     * LoginCommand
     * 
     */

    public class LoginCommand : IServerCommand
    {
        public LoginCommand()
        {
            NeedsAuthcatication = false;
            IsPersistant = false;
        }

        public bool CanHandleCommand(string cmd)
        {
            return cmd.Equals("Login");
        }

        public void HandleCommand(Http http, string cmd = null)
        {
            // @TODO PROPER VALIDATION

            if (string.IsNullOrEmpty(http.Query.Get("Username")) && string.IsNullOrEmpty(http.Query.Get("Password")))
            {
                http.SendXmlResponse(new ErrorXmlResponse {Message = "Missing arguments!"});
                return;
            }

            string username = http.Query.Get("Username");
            string password = http.Query.Get("Password");

            Database db = new Database();
            if (!db.Open())
            {
                Console.WriteLine("FUCK MIG I RØVEN DIN LORTE DB!!!");
            }


            SqlCommand sqlCmd = db.GetCommand();

            sqlCmd.CommandText = "SELECT TOP 1 * FROM Users WHERE username LIKE @username AND password LIKE @password";
            sqlCmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = http.Query.Get("Username");
            sqlCmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = http.Query.Get("Password");

            SqlDataReader r = sqlCmd.ExecuteReader();


            if (r.HasRows)
            {
                // @TODO PROPER HASHING!!
                SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
                byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(username + password));
                string delimitedHexHash = BitConverter.ToString(hash);
                string hexHash = delimitedHexHash.Replace("-", "");


                Database db2 = new Database();
                db2.Open();
                SqlCommand sqlCmdUpdate = db2.GetCommand();
                sqlCmdUpdate.CommandText = "UPDATE Users SET authToken = @authToken";
                sqlCmdUpdate.Parameters.Add("@authToken", SqlDbType.NVarChar).Value = hexHash;
                sqlCmdUpdate.ExecuteNonQuery();
                db2.Close();

                http.SendXmlResponse(new StatusXmlResponse {Code = 200, Message = hexHash, Command = "Login"});
            }
            else
            {
                http.SendXmlResponse(new StatusXmlResponse
                    {
                        Message = "Wrong username or password",
                        Code = 301,
                        Command = "Login"
                    });
            }

            r.Close();
            db.Close();
        }

        public bool NeedsAuthcatication { get; private set; }
        public bool IsPersistant { get; private set; }
    }
}