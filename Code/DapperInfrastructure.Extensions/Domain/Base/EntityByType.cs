namespace DapperInfrastructure.Extensions.Domain.Base
{
     
    public abstract class EntityByType : IEntityByType
    {
        
    }

    /// <summary>
    /// 实体抽象基类
    /// </summary>
    /// <typeparam name="T1"></typeparam>  
    public abstract class EntityByType<T1> : EntityByType
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        protected EntityByType()
        {

        }


        /// <summary>
        /// 主键
        /// </summary>  
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public virtual T1 Id { get; set; }

    }
}
