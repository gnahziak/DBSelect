using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBSelect
{
    /// <summary>
    /// 指定存储的操作是读还是写
    /// </summary>
    public enum SQLRWEnum
    {
        /// <summary>
        /// 不指定
        /// </summary>
        None=1,
        /// <summary>
        /// 写
        /// </summary>
        Write=2,
        /// <summary>
        /// 读
        /// </summary>
        Read=3
    }
}