using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForOurProject.DBConnection
{
    public class DAL
    {
        public enum DBApiOptions
        {
            Neo4jClient,
            Neo4jDriver,
            OrcaleGraph
        }


        #region Singleton

        private static DAL instance = null;

        private DAL()
        {
            NeoDriverConnection.Instance.SetConnectionSettings("Bolt://localhost:7687", "neo4j", "nv1vnmc2");
        }

        public static DAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAL();
                }

                return instance;
            }
        }

        #endregion


        public string QuerySearch(DBApiOptions Options,string query)
        {
            //string result = "";
            switch (Options)
            {
                case DBApiOptions.Neo4jClient:
                    return "";
                case DBApiOptions.Neo4jDriver:
                    return NeoDriverConnection.Instance.QuerySearch(query);
                case DBApiOptions.OrcaleGraph:
                    return "";
            }

            return "";
        }
    }
}
