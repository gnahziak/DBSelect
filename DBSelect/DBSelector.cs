using System;
using System.Data;
using System.Text.RegularExpressions;

namespace DBSelect
{
    public class DBSelector
    {

        /// <summary>
        /// 根据数据库的语句，选择相应的DB（默认 为了兼容之前版本）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static string SelectDB(string sql, CommandType commandType=CommandType.Text, SQLRWEnum rw = SQLRWEnum.None)
        {
            return GetDB(sql, "default", commandType, rw);
        }

        /// <summary>
        /// 选择读写库连接字符串
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="name"></param>
        /// <param name="commandType"></param>
        /// <param name="rw"></param>
        /// <returns></returns>
        public static string SelectDB(string sql, string name, CommandType commandType = CommandType.Text, SQLRWEnum rw = SQLRWEnum.None)
        {
            return GetDB(sql, name, commandType, rw);
        }

        /// <summary>
        /// 选择读写库连接字符串
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="name"></param>
        /// <param name="commandType"></param>
        /// <param name="rw"></param>
        /// <returns></returns>
        public static string GetDB(string sql, string name, CommandType commandType = CommandType.Text, SQLRWEnum rw = SQLRWEnum.None)
        {
            bool IsWrite = false;
            if (rw == SQLRWEnum.Write)
            {
                IsWrite = true;
            }
            else if (rw == SQLRWEnum.Read)
            {
                IsWrite = false;
            }
            else if (commandType == CommandType.StoredProcedure)
            {
                IsWrite = true;//如果是存储过程，则默认是取Writable DB。
            }
            else
            {
                Regex reg = new Regex(@"(\s+|;|\)|')(insert|update|delete)(\s+|\[)", RegexOptions.IgnoreCase);
                sql = " " + sql.ToLower();
                if (reg.IsMatch(sql))
                {
                    IsWrite = true;
                }
            }
            DBConnections db = DBConfiguration.GetConnection(name);

            if (IsWrite)
            {
                return db.WritableDB.ConnectionString;
            }
            else
            {
                return RandomControl.GetRandomReadDB(name);
            }
        }

       

        /// <summary>
        /// 直接指定读写
        /// </summary>
        /// <param name="rw"></param>
        /// <returns></returns>
        public static string SelectDB(SQLRWEnum rw,string name= "default")
        {
            DBConnections db = DBConfiguration.GetConnection(name);

            if (rw==SQLRWEnum.Write)
            {
                return db.WritableDB.ConnectionString;
            }
            else if(rw==SQLRWEnum.Read)
            {
                //return DBConfiguration.ReadDBs[new Random().Next(0, DBConfiguration.ReadDBs.Count)].ConnectionString;
                return RandomControl.GetRandomReadDB(name);
            }
            else
            {
                throw new Exception("参数有误");
            }
        }

    }
}
