using System;
using System.Collections.Generic;
using System.Linq;

namespace DapperInfrastructure.DapperWrapper.SQLHelper
{
    /// <summary>
    /// 存储过程SQL 
    /// </summary>
     public  class ProcSql
    {
        /// <summary>
        /// 存储过程名称
        /// </summary>
        private string _procName;

        /// <summary>
        /// 参数值
        /// </summary>
        private IList<ProcParm> _args;


        public ProcSql()
        {
            _args = new List<ProcParm>();
        }


        /// <summary>
        /// 存储过程名称
        /// </summary>
        public string ProcName
        {
            get { return _procName; }
        }



        public IList<ProcParm> Arguments
        {
            get { return _args; }
        }

        public ProcSql(string name)
        {
            _procName = name;
        }


        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parm"></param>
        public void AddParm(ProcParm parm)
        {
            if (_args == null)
            {
                _args = new List<ProcParm>();
            }
            _args.Add(parm);
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">值</param>
        /// <param name="isOut">是否输出参数</param>
        public void AddParm(string name,object value, bool isOut=false)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new NullReferenceException("存储过程名称不能为null");
            }

            if (_args == null)
            {
                _args = new List<ProcParm>();
            }
            _args.Add(new ProcParm(name,value)
            {
                IsOut =  isOut
            });
        }

        /// <summary>
        /// 获取参数结果值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">参数名</param>
        /// <returns></returns>
        public T GetOutValue<T>(string name)
        {
            if (_args == null)
            {
                return default(T);
            }
            var param = _args.FirstOrDefault(x => x.Name == name);
            if (param != null)
            {
                if (param.OutValue is T)
                {
                    return (T)param.OutValue;
                }
                else
                {
                    try
                    {
                        return (T)Convert.ChangeType(param.OutValue, typeof(T));
                    }
                    catch (InvalidCastException)
                    {
                        return default(T);
                    }
                }
            }
            return default(T); 
       }

        /// <summary>
        /// 存储过程参数
        /// </summary>
        public class  ProcParm
        {
            /// <summary>
            /// 参数名
            /// </summary>
            public readonly string Name;

            /// <summary>
            /// 值
            /// </summary>
            public readonly object Value;


            public ProcParm(string name ,object value)
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new NullReferenceException("存储过程名称不能为null");
                }
                Name = name;
                Value = value;
                IsOut = false;
            }


            /// <summary>
            /// 输出参数值
            /// </summary>
            public  object OutValue { get; set; }
             
            /// <summary>
            /// 是否输出参数
            /// </summary>
            public bool IsOut { get; set; }
        }
    }
}
