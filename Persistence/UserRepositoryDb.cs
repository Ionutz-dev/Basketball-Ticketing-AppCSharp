using System;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using model;

namespace persistence
{
    public class UserRepositoryDb : IUserRepository
    {
        private readonly IDictionary<string, string> props;

        public UserRepositoryDb(IDictionary<string, string> props)
        {
            this.props = props;
        }

        public User FindByUsernameAndPassword(string username, string password)
        {
            using (var con = DbUtils.GetConnection(props))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Users WHERE username = @username AND password = @password";

                    var paramUsername = cmd.CreateParameter();
                    paramUsername.ParameterName = "@username";
                    paramUsername.Value = username;
                    cmd.Parameters.Add(paramUsername);

                    var paramPassword = cmd.CreateParameter();
                    paramPassword.ParameterName = "@password";
                    paramPassword.Value = password;
                    cmd.Parameters.Add(paramPassword);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User(
                                reader.GetInt32(0), // id
                                reader.GetString(1), // username
                                reader.GetString(2) // password
                            );
                        }
                    }
                }
            }

            return null;
        }
    }
}