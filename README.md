# DBSelect
MSSQL数据库读写分离，支持XML配置多实例读写分离。支持读库权重设置。
Nuget上可以直接引用DBSelect

Weight值越大，权重越高。
XML配置如下：
<DBSelect name="default">
    <WritableDB>写库链接字符串</WritableDB>
    <ReadDBs>
      <DB Weight="1">读库链接字符串1</DB>
      <DB Weight="2">读库链接字符串2</DB>
      <DB Weight="3">读库链接字符串3</DB>
      <DB Weight="5">读库链接字符串4</DB>
      <DB Weight="10">读库链接字符串5</DB>
    </ReadDBs>
</DBSelect>
<DBSelect name="other">
    <WritableDB>写库链接字符串</WritableDB>
    <ReadDBs>
      <DB Weight="1">读库链接字符串1</DB>
      <DB Weight="2">读库链接字符串2</DB>
      <DB Weight="3">读库链接字符串3</DB>
      <DB Weight="5">读库链接字符串4</DB>
      <DB Weight="10">读库链接字符串5</DB>
    </ReadDBs>
</DBSelect>


使用方法：
struct DBSelectEnum
{
      public const string Default = "default";
      public const string Learn = "learn";
}
        
1.DBSelector.SelectDB("select * from demo", DBSelectEnum.default)；//自动根据SQL字符串判断读写库选择  默认default节，如果选择其他节则必须显式指定
2.DBSelector.SelectDB(SQLRWEnum.Write); //对于一些查询比较需要及时得语句 可以直接显式指定写库
3.DBSelector.SelectDB("", System.Data.CommandType.StoredProcedure)；//存储过程默认写库  如果读库需显式指定



