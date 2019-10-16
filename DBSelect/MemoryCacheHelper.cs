using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace DBSelect
{
    public class MemoryCacheHelper
    {
        #region 文字介绍

        //一个Cache框架主要包括三个部分：ObjectCache、CacheItemPolicy、ChangeMonitor。
        //ObjectCache表示一个CachePool，它提供了Cache对象的添加、获取、更新等接口，是Cache框架的主体。它是一个抽象类，并且系统给了一个常用的实现——MemoryCache。
        //CacheItemPolicy则表示Cache过期策略，例如保存一定时间后过期。它也经常和ChangeMonitor一起使用，以实现更复杂的策略。
        //ChangeMonitor则主要负责CachePool对象的状态维护，判断对象是否需要更新。它也是一个抽象类，系统也提供了几个常见的实现：CacheEntryChangeMonitor、FileChangeMonitor、HostFileChangeMonitor、SqlChangeMonitor。

        #endregion 文字介绍

        private static ObjectCache obcache = MemoryCache.Default;

        #region 增加内存缓存

        /// <summary>
        /// 增加内存缓存
        /// </summary>
        /// <param name="key">内存缓存键值</param>
        /// <param name="value">内存缓存值</param>
        /// <param name="isAbsoultTime">是否是绝对过期时间</param>
        /// <param name="second">过期时间/秒</param>
        public static void Insert(string key, object value, bool isAbsoultTime, int second)
        {
            //string content = obcache[key] as string;
            if (obcache[key] == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                if (isAbsoultTime)
                {
                    policy.AbsoluteExpiration = DateTime.Now.AddSeconds(second);
                }
                else
                {
                    policy.SlidingExpiration = TimeSpan.FromSeconds(second);
                }
                obcache.Set(key, value, policy);
            }
        }

        #endregion 增加内存缓存

        #region 获取内存缓存值

        /// <summary>
        /// 获取内存缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object getCacheValue(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return null;
            }
            return obcache.Get(key);
        }

        #endregion 获取内存缓存值

        #region 增加文件依赖缓存项
        /// <summary>
        /// 增加文件依赖缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="filePath"></param>
        public static void InsertFileDependency(string key, object value, string filePath)
        {
            if (obcache[key] == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.ChangeMonitors.Add(new HostFileChangeMonitor(new List<string> { filePath }));
                obcache.Set(key, value, policy);
            }
        }
        #endregion

        #region 从缓存项中移除指定项
        /// <summary>
        /// 从缓存项中移除指定项
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveCache(string key)
        {
            if (obcache[key] != null)
            {
                obcache.Remove(key);
            }
        }
        #endregion

    }
}