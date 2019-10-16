using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DBSelect
{
    public class RandomControl
    {

        /// <summary>
        /// 按权重获取随机的读数据库字符串
        /// </summary>
        /// <returns></returns>
        public static string GetRandomReadDB(string name= "default")
        {
            var pre = DBConfiguration.GetConnection(name).ReadDBs;
            if(pre.Count==1)
            {
                return pre[0].ConnectionString;
            }
            var list = Clone(pre);
            Random ran = new Random(GetRandomSeed());
            int totalWeight = list.Sum(c=>c.Weight);
            list.ForEach(c=>c.Weight=(c.Weight+ ran.Next(0,totalWeight)));
            return list.OrderByDescending(c => c.Weight).First().ConnectionString;
        }


        /// <summary>
        /// 随机种子值
        /// </summary>
        /// <returns></returns>
        public static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }


        /// <summary>
        /// 集合深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="RealObject"></param>
        /// <returns></returns>
        public static T Clone<T>(T RealObject)
        {
            using (Stream objectStream = new MemoryStream())
            {
                //利用 System.Runtime.Serialization序列化与反序列化完成引用对象的复制  
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, RealObject);
                objectStream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(objectStream);
            }
        }

    }
}
