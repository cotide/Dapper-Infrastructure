# Dapper-Infrastructure
> Dapper 扩展库 - (Dapper Extensions Library) 

## 优点

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
} 

```

### 分页查询

``` c# 

int pageIndex = 1;
int pageSize = 10;

using (var db = NewDB)
{ 
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
}

```
