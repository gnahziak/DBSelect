using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace DBSelect
{

    public class DBConfiguration
    {
        /// <summary>
        /// 加载XML读写字符串
        /// </summary>
        /// <returns></returns>
        public static DBConnections GetConnection(string name= "default")
        {
            List<DBConnections> conns = new List<DBConnections>();
            var obj = MemoryCacheHelper.getCacheValue("_DBConnection");
            if (obj != null)
            {
                conns = obj as List<DBConnections>;
            }
            else
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "connection.xml");
                XElement root = XElement.Load(path);

                foreach (var elm in root.Elements("DBSelect"))
                {
                    DBConnections conn = new DBConnections();
                    conn.WritableDB = new DB() { ConnectionString = elm.Element("WritableDB").Value };
                    foreach (var item in elm.Element("ReadDBs").Elements("DB"))
                    {
                        DB db = new DB();
                        db.ConnectionString = item.Value;
                        db.Weight = int.Parse(item.Attribute("Weight").Value);
                        conn.ReadDBs.Add(db);
                    }
                    conn.Name = elm.Attribute("name").Value.ToLower(); 
                    conns.Add(conn);
                }

                if (conns.Count == 0)
                    throw new Exception("请配置DBSelect节！");

                MemoryCacheHelper.InsertFileDependency("_DBConnection", conns, path);
            }

            return conns.FirstOrDefault(c => c.Name == name.ToLower());
        }

    }
}
