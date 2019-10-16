using ExamDBSelect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TestDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("110权重概率:" + (10 / 21.0).ToString("P"));
            //Console.WriteLine("105权重概率:" + (5 / 21.0).ToString("P"));
            //Console.WriteLine("103权重概率:" + (3 / 21.0).ToString("P"));
            //Console.WriteLine("102权重概率:" + (2 / 21.0).ToString("P"));
            //Console.WriteLine("101权重概率:" + (1 / 21.0).ToString("P"));
            //Console.WriteLine("\n\n");

            //int count = 3;
            //while (count > 0)
            //{
            //    Stopwatch sw = new Stopwatch();
            //    sw.Start();
            //    List<string> list = new List<string>();
            //    for (int i = 0; i < 10000; i++)
            //    {
            //        list.Add(RandomControl.GetRandomReadDB("learn"));
            //    }
            //    sw.Stop();
            //    Console.WriteLine("执行1万次用时:" + sw.ElapsedMilliseconds + "毫秒");
            //    var g = list.GroupBy(c => c).Select(c => new { count = c.Count(), weight = c.Key }).OrderByDescending(c => c.count);
            //    foreach (var item in g)
            //    {
            //        Console.WriteLine(item.weight + "出现次数:" + item.count + ",出现概率:" + (item.count / 10000.0).ToString("P"));
            //    }
            //    Console.WriteLine("\n\n");
            //    --count;
            //}


            //for (int i = 0; i < 10; i++)
            //{
            //    //选择DB字符串
            //    Console.WriteLine(DBSelector.SelectDB("select * from demo", DBSelectEnum.Learn));//sql语句
            //    //Console.WriteLine(DBSelector.SelectDB("", System.Data.CommandType.StoredProcedure));//存储过程  默认写数据库
            //    Console.WriteLine(DBSelector.SelectDB("", DBSelectEnum.Learn, System.Data.CommandType.StoredProcedure, SQLRWEnum.Read));//存储过程 指定读写
            //    //Console.WriteLine(DBSelector.SelectDB(SQLRWEnum.Write));//直接指定读写
            //}


            //DBSelector.SelectDB("select * from demo", DBSelectEnum.Learn);
            for (int i = 0; i < 100; i++)
            {
                     Console.WriteLine(DBSelector.SelectDB("select * from demo", DBSelectEnum.Learn));
            }
       
            Console.ReadKey();
        }

        struct DBSelectEnum
        {
            public const string Default = "default";
            public const string Learn = "learn";
        }

    }
}
