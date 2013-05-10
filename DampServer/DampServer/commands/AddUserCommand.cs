#region

using System;
using System.Data;
using DampServer.interfaces;

#endregion

namespace DampServer.commands
{
    public class AddUserCommand : IServerCommand
    {
        private ICommandArgument _http;

        public AddUserCommand()
        {
            NeedsAuthcatication = false;
            IsPersistant = false;
        }

        public bool CanHandleCommand(string cmd)
        {
            return cmd.Equals("GetUserByAuthToken") || (cmd.Equals("GetMyUser"));
        }

  

        public void Execute(ICommandArgument http, string cmd)
        {
            _http = http;

            switch (cmd)
            {
                case "AddUser":
                    HandleAddUser();
                    break;
            }
        }

        private void HandleAddUser()
        {
            if (string.IsNullOrEmpty(_http.Query.Get("FirstName")) || string.IsNullOrEmpty(_http.Query.Get("LastName")) ||
                string.IsNullOrEmpty(_http.Query.Get("Sex")) || string.IsNullOrEmpty(_http.Query.Get("Month")) ||
                string.IsNullOrEmpty(_http.Query.Get("Day")) || string.IsNullOrEmpty(_http.Query.Get("Year")) ||
                string.IsNullOrEmpty(_http.Query.Get("UserName")) || string.IsNullOrEmpty(_http.Query.Get("Password")) ||
                string.IsNullOrEmpty(_http.Query.Get("Email")) || string.IsNullOrEmpty(_http.Query.Get("Language")) ||
                string.IsNullOrEmpty(_http.Query.Get("City"))  ||
                string.IsNullOrEmpty(_http.Query.Get("Description")))
            {
                _http.SendXmlResponse(new ErrorXmlResponse
                    {
                        Message = "Missing arguments"
                    });
               
                return;
            }

            var db = new Database();
            db.Open();

            var sql = db.GetCommand();

            var photo = _http.Query.Get("Photo");
            Console.WriteLine(photo);

            sql.CommandText =
                "INSERT INTO Users (username, password, email, country," +
                " language, city, gender, description) VALUES(@username," +
                " @password, @email, @country, @language, @city, @gender, @description )";

            sql.Parameters.Add("@username", SqlDbType.NVarChar).Value = _http.Query.Get("UserName");
            sql.Parameters.Add("@password", SqlDbType.NVarChar).Value = _http.Query.Get("Password");
            sql.Parameters.Add("@email", SqlDbType.NVarChar).Value = _http.Query.Get("Email");
            sql.Parameters.Add("@country", SqlDbType.NVarChar).Value = _http.Query.Get("Country");
            sql.Parameters.Add("@language", SqlDbType.NVarChar).Value = _http.Query.Get("Language");
            sql.Parameters.Add("@city", SqlDbType.NVarChar).Value = _http.Query.Get("City");
            sql.Parameters.Add("@gender", SqlDbType.NVarChar).Value = _http.Query.Get("Sex");
      //      sql.Parameters.Add("@photo", SqlDbType.NVarChar).Value = _http.Query.Get("Photo");
            sql.Parameters.Add("@description", SqlDbType.Text).Value = _http.Query.Get("Description");

            sql.ExecuteNonQuery();

            _http.SendXmlResponse(new StatusXmlResponse
            {
                Code = 200,
                Command = "AddUser",
                Message = "User added"
            });

        }

        public bool NeedsAuthcatication { get; private set; }
        public bool IsPersistant { get; private set; }
    }
}