using System;
using System.Linq;
using BH.Domain.Entity.Category;
using DapperInfrastructure.DapperWrapper.SQLHelper;
using DapperInfrastructure.Tests.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DapperInfrastructure.Tests.DB
{
    /// <summary>
    /// DBTest 的摘要说明
    /// </summary>
    [TestClass]
    public class DBTest  
    { 
         
        [TestInitialize]
        public void Init()
        {

        }


        [TestMethod]
        public void Insert()
        {
            // 新增 应用分类
            using (var db = NewDB)
            {
                var repository = db.GetRepository<ApplicationMtr>();
                repository.Create(new ApplicationMtr()
                {
                    Name = "Test1"
                });
            }  
            
        }


        /// <summary>
        /// 新增 - 事务
        /// </summary> 
        [TestMethod]
        public void Insert_Transaction()
        {  
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

        }




        /// <summary>
        /// 新增 - 事务
        /// </summary> 
        [TestMethod]
        public void Insert2_Transaction()
        {
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

        }



        /// <summary>
        /// 查询  
        /// </summary>
        [TestMethod]
        public void SqlQueryTest()
        {
            using (var db = NewDB)
            {   
                // 方式1
                var result =   db.GetSqlQuery.GetList<ApplicationMtr>(
                    "select * from  ApplicationMTR where Name = @0 ", "Game");
                Console.WriteLine(result.Count());
                Assert.IsTrue(result.Count()>0);

                // 方式2
                result = db.GetSqlQuery.GetList<ApplicationMtr>(
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


        }


        /// <summary>
        /// 查询列表
        /// </summary>
        [TestMethod]
        public void GetListTest()
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



        /// <summary>
        /// 分页查询
        /// </summary>
        [TestMethod]
        public void SqlPagingForOneTableTest()
        {
            int pageIndex = 1;
            int pageSize = 10;

            using (var db = NewDB)
            {
               // 单表分页查询 方式1
                var result = db.GetRepository<ApplicationMtr>()
                   .PageList<ApplicationMtr>(pageIndex, pageSize,
                   Sql.Builder.Append(" select * from  ApplicationMTR ")); 
                Console.WriteLine(result.TotalCount);
                Assert.IsTrue(result.TotalCount > 0);   

                // 单表分页查询 方式2
                result = db.GetRepository<ApplicationMtr>()
                    .PageList<ApplicationMtr>(pageIndex, pageSize,
                        Sql.Builder.SelectAll().From<ApplicationMtr>());
                Console.WriteLine(result.TotalCount);
                Assert.IsTrue(result.TotalCount > 0);
            }
        }


        /// <summary>
        ///  多条件 分页查询
        /// </summary>
        [TestMethod]
        public void SqlPagingForManyTableTest()
        {
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
                var result2 = db.GetSqlQuery.PageList<MyDtoClass>(
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
        }




        /// <summary>
        /// 多条件 分页查询 
        /// 处理Select Distinct  分页查询
        /// </summary>
        [TestMethod]
        public void SqlPagingForManyTableDistinctTest()
        {
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
                var result2 = db.GetSqlQuery.PageList<MyDtoClass>(
                    pageIndex,
                    pageSize,
                    sql);
                 
                Console.WriteLine(result2.TotalCount);
                Assert.IsTrue(result2.TotalCount > 0);
            }
        }


        /// <summary>
        /// 执行删除SQL
        /// </summary>
        [TestMethod]
        public void DeleteTest()
        {
            using (var db = NewDB)
            {
                var sql = Sql.Builder.Append(
                    string.Format("DELETE FROM {0} WHERE Id = @0", db.GetTableName<ApplicationMtr>()), 1);
                db.GetSqlRun.Execute(sql);
            }
        }



        [TestMethod]
        public void GetProc()
        {
            /*
               USE [CommonDB]
               GO
               CREATE proc[dbo].[TestProc1]
               AS
               BEGIN
                    SELECT   'Test' AS RESULT
               END
            */
            using (var db = NewDB)
            {

                var procSql = new ProcSql("TestProc1");
                var result = db.GetSqlRun.ExecuteProcObj<string>(procSql);
                Console.WriteLine(string.Format("Result = {0}", result));
            }
             
            /*
                USE [CommonDB]
                GO 
                CREATE proc[dbo].[TestProc]
                (
                     @Value nvarchar(255),
                     @OutValue nvarchar(255)   output
                )
                AS
                BEGIN
                    SET @OutValue = 'Hello World!'
                    SELECT @Value  AS RESULT
                END
             */
            using (var db = NewDB)
            {

                var procSql = new ProcSql("TestProc");
                procSql.AddParm("Value", "Hello Value");
                procSql.AddParm("OutValue", "Hello OutValue", true);
                var result = db.GetSqlRun.ExecuteProcObj<string>(procSql);
                Console.WriteLine(string.Format("Result = {0}", result));
                Console.WriteLine(String.Format("OutValue = {0}", procSql.GetOutValue<string>("OutValue")));
            }
             
            /*
                USE [CommonDB]
                USE [CommonDB]
                GO
                // 测试表1
                CREATE TABLE [dbo].[Test](
	                [Name] [nchar](10) NULL
                ) ON [PRIMARY] 
                GO
                // 测试表2
                CREATE TABLE [dbo].[Test1](
	                [Name] [nchar](10) NULL,
	                [Age] [int] NULL
                ) ON [PRIMARY]   
                GO 
                // 测试存储过程
                CREATE proc  [dbo].[TestProc2]
                (
                    @Value    nvarchar(255) 
                   ,@OutValue   nvarchar(255)   output
                )
                AS
                BEGIN 
                  SET @OutValue =  (SELECT top 1 name FROM CommonDB.dbo.Test)
                  SELECT * FROM CommonDB.dbo.Test1
                END
             */
            using (var db = NewDB)
            {

                var procSql = new ProcSql("TestProc2");
                procSql.AddParm("Value", "Hello Value");
                procSql.AddParm("OutValue", "Hello OutValue", true);
                var result = db.GetSqlRun.ExecuteProcList<string>(procSql);
                Console.WriteLine(string.Format("Result = {0}", result));
                foreach (var item in result)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine(String.Format("OutValue = {0}", procSql.GetOutValue<string>("OutValue")));
            }
             
        }


        #region Helper

        /// <summary>
        /// Dto 对象
        /// </summary>
        public class MyDtoClass
        {
            public string Id { get; set; }

            public string ApplicationMtr_Name { get; set; }

            public string ApplicationMtr_CategoryId { get; set; }

            public DateTime ApplicationMtr_CreateTime { get; set; }

            public string CategoryApplicationMtr_Name { get; set; }
        }



        private DapperInfrastructure.DB NewDB 
        {
            get { return DapperInfrastructure.DB.New("default"); }
        }


        private DapperInfrastructure.DB GetNewDB(DBName dbName)
        {
            return DapperInfrastructure.DB.New(dbName.ToString());
        }

        #endregion

    }
}
