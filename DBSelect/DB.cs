using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBSelect
{
    [Serializable]
    public class DB
    {
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 权重
        /// </summary>
        public int Weight { get; set; }
    }

    /// <summary>
    /// 数据库读写字符串
    /// </summary>
    [Serializable]
    public class DBConnections
    {
        /// <summary>
        /// 写库
        /// </summary>
        public DB WritableDB { get; set; }

        /// <summary>
        /// 读库
        /// </summary>
        public List<DB> ReadDBs { get; set; } = new List<DB>();

        /// <summary>
        /// 选择项名称
        /// </summary>
        public string Name { get; set; }
    }

}
