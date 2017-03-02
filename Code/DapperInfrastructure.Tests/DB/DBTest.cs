using System;
using System.Linq;
using BH.Domain.Entity.Category;
using DapperInfrastructure.DapperWrapper.SQLHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DapperInfrastructure.Tests.DB
{
    /// <summary>
    /// DBTest 的摘要说明
    /// </summary>
    [TestClass]
    public class DBTest  
    { 
        #region MySQL  
        public static readonly string MySqlConntion = @"server=23.106.153.199;user id=root;password=88888888;persistsecurityinfo=True;database=cotide_test;charset=utf8;";  
        public static readonly string MySqlProviderName = "MySql.Data.MySqlClient"; 
        #endregion

         
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
        /// 查询  
        /// </summary>
        [TestMethod]
        public void SqlQueryTest()
        {
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
        }




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
               // 单表分页查询
                var result = db.GetRepository<ApplicationMtr>()
                   .PageList<ApplicationMtr>(pageIndex, pageSize,
                   Sql.Builder.Append(" select * from  ApplicationMTR ")); 
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
                var result2 = db.SqlQuery.PageList<MyDtoClass>(
                    pageIndex,
                    pageSize,
                    sql);
                 
                Console.WriteLine(result2.TotalCount);
                Assert.IsTrue(result2.TotalCount > 0);
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
            get { return DapperInfrastructure.DB.New(MySqlConntion, MySqlProviderName); }
        } 

        #endregion

    }
}
