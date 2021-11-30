using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbWorker.Tables;

namespace DbWorker
{
    public class DbManager
    {
        private static DbManager instance = null;

        public static DbManager GetInstance()
        {
            if (instance == null)
            {
                instance = new DbManager();
            }

            return instance;
        }

        public TableUsers TableUsers { get; private set; }
        public TableItems TableItems { get; private set; }

        private DbManager()
        {
            TableItems = new TableItems();
            TableUsers = new TableUsers();
        }

    }
}
