using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbEntities;
using DbWorker.Tools;
using MySql.Data.MySqlClient;

namespace DbWorker.Tables
{
    public class TableItems
    {
        public List<Item> GetCardsByUserId(int userId)
        {
            try
            {
                List<Item> items = new List<Item>();

                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $@"
                        SELECT i.id FROM `users_items` AS ui 
                        JOIN `items` AS i ON ui.id_item = i.id
                        WHERE `id_user`= {userId};";

                        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                        {
                            while (mySqlDataReader.Read() == true)
                            {
                                items.Add(new Item()
                                {
                                    Id = mySqlDataReader.GetInt32("id"),
                                });
                            }
                        }
                    }
                }

                return items;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //public bool IsMoneyAvaible(int userId, )

        public void BuyItem(int userId, int itemId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();
                    using (MySqlTransaction mySqlTransaction = mySqlConnection.BeginTransaction())
                    {
                        using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                        {
                            mySqlCommand.Transaction = mySqlTransaction;
                            try
                            {
                                mySqlCommand.CommandText = $"UPDATE `items` SET `count` = `count` - 1 WHERE `id`={itemId};";
                                mySqlCommand.ExecuteNonQuery();

                                //mySqlCommand.CommandText = $"UPDATE `users` SET `money` = `money` - {} WHERE `id`={userId};";
                                //mySqlCommand.ExecuteNonQuery();

                                mySqlCommand.CommandText = $"INSERT INTO users_items (id_user,id_item) VALUES({userId}, {itemId});";
                                mySqlCommand.ExecuteNonQuery();

                                mySqlTransaction.Commit();
                            }
                            catch (Exception e)
                            {
                                mySqlTransaction.Rollback();
                                throw e;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ExistsCardByNumber(int itemId)
        {
            try
            {
                bool exists = false;

                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"SELECT * FROM `items` WHERE `id`={itemId};";

                        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                        {
                            exists = mySqlDataReader.HasRows;
                        }
                    }
                }

                return exists;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

