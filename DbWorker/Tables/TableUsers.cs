using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbEntities;
using DbWorker.Tools;
using MySql.Data.MySqlClient;
using Ubiety.Dns.Core.Records.NotUsed;
using ExceptionEntities;

namespace DbWorker.Tables
{
    public class TableUsers
    {
        public User GetUserByLoginPassword(string login, string password)
        {
            try
            {
                User user = null;

                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"SELECT * FROM `users` WHERE `login`='{login}' AND `password`='{password}';";

                        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                        {
                            if (mySqlDataReader.HasRows == true)
                            {
                                mySqlDataReader.Read();

                                user = new User()
                                {
                                    Id = mySqlDataReader.GetInt32("id"),
                                    Name = mySqlDataReader.GetString("name"),
                                    Login = mySqlDataReader.GetString("login"),
                                    Password = mySqlDataReader.GetString("password"),
                                    Money = mySqlDataReader.GetDecimal("money")
                                };
                            }
                            else
                            {
                                throw new WrongLoginPasswordException();
                            }
                        }
                    }
                }

                return user;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}