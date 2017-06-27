using Neo4j.Driver.V1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForOurProject.DBConnection
{
    public class NeoDriverConnection : IQueries
    {
        #region DataMembers

        private IDriver _database { get; set; }
        private ISession _session { get; set; }

        #endregion

        #region Singleton

        private static NeoDriverConnection instance = null;

        private NeoDriverConnection()
        {

        }

        public static NeoDriverConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NeoDriverConnection();
                }

                return instance;
            }
        }

        #endregion

        #region Settings/Connection

        public void SetConnectionSettings(string connectionString, string username, string password)
        {
            _database = GraphDatabase.Driver(connectionString, AuthTokens.Basic(username, password), Config.DefaultConfig);
        }

        public void Connect()
        {
            _session = _database.Session();
        }

        public void Dispose()
        {
            _session?.Dispose();
        }

        #endregion

        #region CrudNeoConnect

        public string QuerySearch(string query)
        {
            var Data = new List<IReadOnlyDictionary<string, object>>();

            Connect();

            IStatementResult results = NeoDriverConnection.Instance._session.Run(query);
            Dictionary<string, object> dict = new Dictionary<string, object>();
            foreach (IRecord node in results)
            {

                if ((node[node.Keys[0]] as INode) != null)
                {
                    dict = (Dictionary<string, object>)(node[node.Keys[0]] as INode).Properties;
                    dict.Add("label", (node[node.Keys[0]] as INode).Labels[0]);
                    Data.Add(dict);
                }

                dict = new Dictionary<string, object>();
            }

            IStatementResult relationsFinder = NeoDriverConnection.Instance._session.Run(query);

            foreach (IRecord node in relationsFinder)
            {
                if ((node[node.Keys[0]] as IRelationship) != null)
                {
                    Data.Add((Dictionary<string, object>)(node[node.Keys[0]] as IRelationship).Properties);
                }
            }

            Dispose();

            return JsonConvert.SerializeObject(Data);
        }

        public string GetLabelsNames()
        {
            var Data = new Dictionary<string, object>();

            Connect();

            IStatementResult results = NeoDriverConnection.Instance._session.Run($"MATCH (n) RETURN distinct labels(n)[0] as tables,  count(*) as tablesObjectNumbers union MATCH ()-[x]->()   return  'Relations' as tables, count(x) as tablesObjectNumbers");

            foreach (IRecord node in results)
            {
                string tableName = (node[node.Keys[0]]).ToString();
                object tableCount = (node[node.Keys[1]] as object).ToString();
                Data.Add(tableName, tableCount);
            }

            Dispose();

            return JsonConvert.SerializeObject(Data);
        }

        public string GetDataByLabelName(string LabelName)
        {
            var Data = new List<IReadOnlyDictionary<string, object>>();

            Connect();

            IStatementResult results = NeoDriverConnection.Instance._session.Run($"MATCH(n:{LabelName} ) return(n) as nodes");

            foreach (IRecord node in results)
            {
                Data.Add((node["nodes"] as INode).Properties);
            }

            IStatementResult relationsFinder = NeoDriverConnection.Instance._session.Run($"MATCH() - [r:{LabelName}] - () RETURN DISTINCT(r) as nodes");

            foreach (IRecord node in relationsFinder)
            {
                Data.Add((node["nodes"] as IRelationship).Properties);
            }

            Dispose();

            return JsonConvert.SerializeObject(Data);
        }

        public string GetDataByLabelNameFillteredWithAttributeName(string LabelName, Dictionary<string, List<string>> fillterNameWithValues)
        {
            var Data = new List<IReadOnlyDictionary<string, object>>();

            Connect();

            #region NodeFillter

            IStatementResult results = NeoDriverConnection.Instance._session.Run($"MATCH(n:{LabelName} ) return(n) as nodes");

            bool bToAdd = true;
            foreach (IRecord node in results)
            {
                foreach (string attributeName in fillterNameWithValues.Keys)
                {
                    if ((node["nodes"] as INode).Properties.Keys.Contains(attributeName) && !(fillterNameWithValues[attributeName].Contains((node["nodes"] as INode).Properties[attributeName])))
                    {
                        bToAdd = false;
                    }
                }

                if (bToAdd)
                {
                    Data.Add((node["nodes"] as INode).Properties);
                }

                bToAdd = true;
            }
            #endregion

            #region RelationFillter

            IStatementResult relationsFinder = NeoDriverConnection.Instance._session.Run($"MATCH() - [r:{LabelName}] - () RETURN (r) as nodes");

            foreach (IRecord node in relationsFinder)
            {
                foreach (string attributeName in fillterNameWithValues.Keys)
                {
                    if ((node["nodes"] as IRelationship).Properties.Keys.Contains(attributeName) && !(fillterNameWithValues[attributeName].Contains((node["nodes"] as IRelationship).Properties[attributeName])))
                    {
                        bToAdd = false;
                    }
                }

                if (bToAdd)
                {
                    Data.Add((node["nodes"] as IRelationship).Properties);
                }

                bToAdd = true;
            }
            #endregion

            Dispose();

            return JsonConvert.SerializeObject(Data);
        }

        public string GetRelationsByObjectId(string labelName, string objectId)
        {
            var Data = new List<IReadOnlyDictionary<string, object>>();

            Connect();

            IStatementResult results = NeoDriverConnection.Instance._session.Run($"match(n:{labelName})-[x]-(r) where n._id = '{objectId}'  return r as nodes");

            foreach (IRecord node in results)
            {
                Data.Add((node["nodes"] as INode).Properties);
            }

            Dispose();

            return JsonConvert.SerializeObject(Data);
        }

        public string GetRelationsByObjectIdAndFillterRelationsWithTableName(string labelName, string objectId, string relatedTableName)
        {
            var Data = new List<IReadOnlyDictionary<string, object>>();

            Connect();

            IStatementResult results = NeoDriverConnection.Instance._session.Run($"match(n:{labelName})-[x]-(r:{relatedTableName}) where n._id = '{objectId}' return r as nodes");

            foreach (IRecord node in results)
            {
                Data.Add((node["nodes"] as INode).Properties);
            }

            Dispose();

            return JsonConvert.SerializeObject(Data);
        }

        public string GetRelationsByObjectIdAndFillterRelationsWithParameter(string labelName, string objectId, string attributeName, string attributeValue)
        {
            var Data = new List<IReadOnlyDictionary<string, object>>();

            Connect();

            IStatementResult results = NeoDriverConnection.Instance._session.Run($"match(n:{labelName})-[x]-(r) where n._id = '{objectId}' AND r.{attributeName} = '{attributeValue}'  return r as nodes");

            foreach (IRecord node in results)
            {
                Data.Add((node["nodes"] as INode).Properties);
            }

            Dispose();

            return JsonConvert.SerializeObject(Data);
        }

        public void CreateNode(string labelName, Dictionary<string, object> values)
        {
            Connect();

            #region Building The Query

            StringBuilder query = new StringBuilder($"CREATE(:{labelName}" + "{");

            foreach (string key in values.Keys)
            {
                query.Append(key.ToString() + ":'" + values[key].ToString() + "',");
            }

            query.Remove(query.Length - 1, 1);
            query.Append("})");

            #endregion

            NeoDriverConnection.Instance._session.Run(query.ToString());

            Dispose();
        }

        public void CreateRelation(string tableName, string objectId, string sourceObjectId, string destObjectId, string relationName)
        {
            Connect();

            NeoDriverConnection.Instance._session.Run("MATCH(sourceNode{_id:" + $"'{sourceObjectId}'" + "}),(destNode{_id:" + $"'{destObjectId}'" + "})" + $" Create(sourceNode) -[:{tableName}" + "{" + $"name:'{relationName}'," + $"_id:'{objectId}'" + "}]->(destNode)");

            Dispose();
        }

        public void UpdateNode(string collectionName, string objectId, Dictionary<string, object> values)
        {
            Connect();

            #region Building The Query For Nodes

            StringBuilder query = new StringBuilder($"MATCH(node:{collectionName}) WHERE node._id = '{objectId}' SET ");

            foreach (string key in values.Keys)
            {
                query.Append("node." + key.ToString() + "='" + values[key].ToString() + "',");
            }

            query.Remove(query.Length - 1, 1);

            #endregion

            NeoDriverConnection.Instance._session.Run(query.ToString());

            #region Building The Query For Relations

            query = new StringBuilder($"MATCH(() -[node:{collectionName}] -()) WHERE node._id = '{objectId}' SET ");

            foreach (string key in values.Keys)
            {
                query.Append("node." + key.ToString() + "='" + values[key].ToString() + "',");
            }

            query.Remove(query.Length - 1, 1);

            #endregion

            NeoDriverConnection.Instance._session.Run(query.ToString());

            Dispose();
        }

        public void DeleteNodeByObjectID(string objectId)
        {
            Connect();

            NeoDriverConnection.Instance._session.Run($"MATCH(n) where n._id = '{objectId}' detach delete(n)");

            NeoDriverConnection.Instance._session.Run($"MATCH() - [relation] - () WHERE relation._id = '{objectId}' detach delete(relation)");

            Dispose();
        }

        void IQueries.QuerySearch(string query)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
