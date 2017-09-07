# Dapper-Infrastructure
> Dapper 扩展库 - (Dapper Extensions Library) 


## 文件结构说明

* /Code/... 程序文件
* /DB/Init_DB.sql MYSQL初始化脚本

## 特点

* 集成PetaPoco的SQL Linq语法糖.
* 支持多表关联分页查询.
* CRUD 封装/简化调用方式.


## 事例


### 新增实体

``` c#
 
using (var db = NewDB)
{
	var repository = db.GetRepository<ApplicationMtr>();
	repository.Create(new ApplicationMtr()
	{
		Name = "Test1"
	});
}  

```

### 新增实体 - 事务

``` c#
 
using (var db = NewDB)
{  
	// 开始事务
	db.BeginTransaction();

	var repository = db.GetRepository<ApplicationMtr>();  

	repository.Create(new ApplicationMtr()
	{
		Name = "Game"
	}); 
		
	repository.Create(new ApplicationMtr()
	{
		Name = "Work"
	});
		
	repository.Create(new ApplicationMtr()
	{
		Name = "Book"
	});

	// 事务提交
	db.Commit();  
} 

```


### 新增 - 事务 (多数据库实例切换)

``` c#
 
 using (var db = NewDB)
 {
    // 开始事务
    db.BeginTransaction();

    var repository = db.GetRepository<ApplicationMtr>();

    repository.Create(new ApplicationMtr()
    {
        Name = "Game"
    });
    // 切换数据库实例
    db.ChangeDatabase(DBName.DB1.ToString()); 
    repository.Create(new ApplicationMtr()
    {
        Name = "Work"
    });
    // 切换数据库实例
    db.ChangeDatabase(DBName.DB2.ToString()); 
    repository.Create(new ApplicationMtr()
    {
        Name = "Book"
    }); 
    // 事务提交
    db.Commit();
 }

```


### 查询

``` c#
 
using (var db = NewDB)
{   
	// 方式1
	var result =   db.SqlQuery.GetList<ApplicationMtr>(
		"select * from  ApplicationMTR where Name = @0 ", "Game");
	Console.WriteLine(result.Count());
	Assert.IsTrue(result.Count()>0);

	// 方式2
	result = db.SqlQuery.GetList<ApplicationMtr>(
		Sql.Builder.Append("select * from  ApplicationMTR ")
		.Where(" Name = @0 ","Game")
		); 
	Console.WriteLine(result.Count());
	Assert.IsTrue(result.Count() > 0); 

    // 方式3
    result = db.GetSqlQuery.GetList<ApplicationMtr>(
        Sql.Builder.SelectAll().From<ApplicationMtr>()
        .Where(" Name = @0 ", "Game"));
    Console.WriteLine(result.Count());
    Assert.IsTrue(result.Count() > 0);
} 

```

### 查询

``` c#
 
using (var db = NewDB)
{   
    var sql = new Sql("SELECT * FROM CategoryApplicationMTR ");
    sql.Where(" Name = @0", "A1");
    sql.Where(" Name  = @0 ", "A2");
    sql.Where(" Name  = @0 ", "A3");
    sql.WhereIfIn(" Name ", new[] { "A4", "A5", "A6" });
    sql.OrderBy(" Name desc ");
    var repository = NewDB.GetRepository<CategoryApplicationMtr>();
    var result = repository.GetList<CategoryApplicationMtr>(sql);
    Assert.IsTrue(!result.Any());
} 

```
 

### 分页查询

``` c# 

int pageIndex = 1;
int pageSize = 10;

using (var db = NewDB)
{ 
    // 方式1
	var sql = Sql.Builder.Append(" select a.Id," +
									" a.`Name` as ApplicationMtr_Name, " +
									"a.CategoryId as ApplicationMtr_CategoryId," +
									" a.CreateTime as ApplicationMtr_CreateTime, " +
									"c.`Name` as CategoryApplicationMtr_Name   " +
									" from ApplicationMTR as a" +
									" left join CategoryApplicationMTR as c on a.CategoryId = c.Id");
	sql.Where(" a.Name = @0 ", "Game");

	// 多表分页查询
	var result2 = db.SqlQuery.PageList<MyDtoClass>(
		pageIndex,
		pageSize,
		sql);

	Console.WriteLine(result2.TotalCount);
	Assert.IsTrue(result2.TotalCount > 0); 

    // 方式2 
    sql = Sql.Builder.Select("a.id,a.`Name` as ApplicationMtr_Name," +
                             "a.CategoryId as ApplicationMtr_CategoryId," +
                             "a.CreateTime as ApplicationMtr_CreateTime," +
                             "c.`Name` as CategoryApplicationMtr_Name ")
                             .From<ApplicationMtr>("a")
                             .LeftJoin<CategoryApplicationMtr>("c").On("a.CategoryId = c.Id")
                             .Where(" a.Name = @0 ", "Game");

    result2 = db.GetSqlQuery.PageList<MyDtoClass>(
        pageIndex,
        pageSize,
        sql);
 
    Console.WriteLine(result2.TotalCount);
    Assert.IsTrue(result2.TotalCount > 0); 

}

```


### 分页查询 (Select Distinct)

``` c# 

int pageIndex = 1;
int pageSize = 10;

using (var db = NewDB)
{
    var sql = Sql.Builder.Append(" select distinct a.Id," +
                                 " a.`Name` as ApplicationMtr_Name, " +
                                 "a.CategoryId as ApplicationMtr_CategoryId," +
                                 " a.CreateTime as ApplicationMtr_CreateTime, " +
                                 "c.`Name` as CategoryApplicationMtr_Name   " +
                                 " from ApplicationMTR as a" +
                                 " left join CategoryApplicationMTR as c on a.CategoryId = c.Id");
    sql.Where(" a.Name = @0 ", "Game");
    // 处理 Select Count Distinct 情况
    sql.SetCountField(" distinct a.Id ");
    
    // 多表分页查询
    var result2 = db.SqlQuery.PageList<MyDtoClass>(
        pageIndex,
        pageSize,
        sql);
     
    Console.WriteLine(result2.TotalCount);
    Assert.IsTrue(result2.TotalCount > 0);
}

```



### SQL Execute


``` c# 

using (var db = NewDB)
{
    var sql = Sql.Builder.Append(
        string.Format("DELETE FROM {0} WHERE Id = @0", db.GetTableName<ApplicationMtr>()), 1);
    db.GetSqlRun.Execute(sql);
}

```


## 参考资料
* [Dapper](https://github.com/StackExchange/Dapper)
* [PetaPoco](https://github.com/CollaboratingPlatypus/PetaPoco)
