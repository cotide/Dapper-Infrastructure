using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DapperInfrastructure.Extensions.Domain;
using DapperInfrastructure.Extensions.Domain.Base;
using DapperInfrastructure.Extensions.Mapper;

namespace DapperInfrastructure.DapperWrapper.SQLHelper
{  
    /// <summary>
    /// SQL Class
    /// </summary>
    public class Sql
    { 
        public Sql()
        { 

        }

 
        public Sql(string sql, params object[] args)
        {
            _sql = sql;
            _args = args; 
        } 
 

        public static Sql Builder
        {
            get { return new Sql(); }
        }



        string _sql; 
        string _sqlFinal; 
        Sql _obj; 
        object[] _args;  
        object[] _argsFinal; 
        string countField;

        /// <summary>
        /// 总数查询字段
        /// </summary>
        public string CountField
        {
            get
            {
                return countField;
            }
        }


        public Sql SetCountField(string field)
        {
            this.countField = field;
            return this;
        }



        private void Build()
        {
            // already built?
            if (_sqlFinal != null)
                return;

            // Build it
            var sb = new StringBuilder();
            var args = new List<object>();
            Build(sb, args, null);
            _sqlFinal = sb.ToString();
            _argsFinal = args.ToArray();
        }

         
        private void Build(
            StringBuilder sb, 
            List<object> args, 
            Sql lhs)
        {
            if (!String.IsNullOrEmpty(_sql))
            {
                // Add SQL to the string
                if (sb.Length > 0)
                {
                    sb.Append("\n");
                }

                var sql = ProcessParams(_sql, _args, args);

                if (Is(lhs, "WHERE ") && Is(this, "WHERE "))
                    sql = "AND " + sql.Substring(6);
                if (Is(lhs, "ORDER BY ") && Is(this, "ORDER BY "))
                    sql = ", " + sql.Substring(9);

                sb.Append(sql);
            }

            // Now do rhs
            if (_obj != null)
                _obj.Build(sb, args, this);
        }

         
        static Regex rxParams = new Regex(@"(?<!@)@\w+", RegexOptions.Compiled);
        private string ProcessParams(
            string _sql,
            object[] args_src,
            List<object> args_dest)
        {
            return rxParams.Replace(_sql, m =>
            {
                string param = m.Value.Replace("arg", "").Substring(1);

                object arg_val; 
                int paramIndex;
                if (int.TryParse(param, out paramIndex))
                {
                    // Numbered parameter
                    if (paramIndex < 0 || paramIndex >= args_src.Length)
                        throw new ArgumentOutOfRangeException(string.Format("Parameter '@{0}' specified but only {1} parameters supplied (in `{2}`)", paramIndex, args_src.Length, _sql));
                    arg_val = args_src[paramIndex];
                }
                else
                {
                    // Look for a property on one of the arguments with this name
                    bool found = false;
                    arg_val = null;
                    foreach (var o in args_src)
                    {
                        var pi = o.GetType().GetProperty(param);
                        if (pi != null)
                        {
                            arg_val = pi.GetValue(o, null);
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                        throw new ArgumentException(string.Format("Parameter '@{0}' specified but none of the passed arguments have a property with this name (in '{1}')", param, _sql));
                }

                // Expand collections to parameter lists
                if ((arg_val as System.Collections.IEnumerable) != null &&
                    (arg_val as string) == null &&
                    (arg_val as byte[]) == null)
                {
                    var sb = new StringBuilder();
                    foreach (var i in arg_val as System.Collections.IEnumerable)
                    {
                        sb.Append((sb.Length == 0 ? "@arg" : ",@arg") + args_dest.Count.ToString());
                        args_dest.Add(i);
                    }
                    return sb.ToString();
                }
                else
                {
                    args_dest.Add(arg_val);
                    return "@arg" + (args_dest.Count - 1).ToString();
                }
              }
            );
        }


        public string SQL
        {
            get
            {
                Build();
                return _sqlFinal;
            }
        }

        public object[] Arguments
        {
            get
            {
                Build();
                return _argsFinal;
            }
        } 


        public Sql Append(Sql sql)
        {
            if (_obj != null)
                _obj.Append(sql);
            else
                _obj = sql;

            return this;
        }


        public Sql AppendFormat(string sql, params object[] args)
        { 
            return Append(new Sql(string.Format(sql, args)));
        }
          
      


        public Sql Append(string sql, params object[] args)
        {
            return Append(new Sql(sql, args));
        }

        static bool Is(Sql sql, string sqltype)
        {
            return sql != null && sql._sql != null && sql._sql.StartsWith(sqltype, StringComparison.InvariantCultureIgnoreCase);
        }

        public Sql Where(string sql, params object[] args)
        {
            return Append(new Sql("WHERE (" + sql + ")", args));
        }

        public Sql OrderBy(params object[] columns)
        {
            return Append(new Sql("ORDER BY " + String.Join(", ", (from x in columns select x.ToString()).ToArray())));
        }

        public Sql Select(params object[] columns)
        {
            return Append(new Sql("SELECT " + String.Join(", ", (from x in columns select x.ToString()).ToArray())));
        }



        public Sql SelectTop(int top, params object[] columns)
        {
            if (columns.Any())
            {
                return Append(new Sql("SELECT top " + top + " " + String.Join(", ", (from x in columns select x.ToString()).ToArray())));
            }
            else
            {
                return Append(new Sql("SELECT top " + top + " * "));
            }

        }

        public Sql SelectAll(params object[] columns)
        {
            return Append(new Sql("SELECT  * " ));
        }

        public Sql From(params object[] tables)
        {
            return Append(new Sql("FROM " + String.Join(", ", (from x in tables select x.ToString()).ToArray())));
        }
        



        public Sql FromDB<T>(string dbName, string asName = null) where T : EntityByType
        {
            return Append(new Sql("FROM " + dbName + ".dbo." + TableMaper.GetName<T>() + " " + asName));
        }

        public Sql From<T>(string asName = null) where T : EntityByType
        {
            return Append(new Sql("FROM " +  TableMaper.GetName<T>() +" "+ asName ));
        } 

        public Sql GroupBy(params object[] columns)
        {
            return Append(new Sql("GROUP BY " + String.Join(", ", (from x in columns select x.ToString()).ToArray())));
        }

        private SqlJoinClause Join(string JoinType, string table)
        {
            return new SqlJoinClause(Append(new Sql(JoinType + table)));
        }

        public SqlJoinClause InnerJoin(string table)
        {
            return Join("INNER JOIN ", table);
        }

        public SqlJoinClause LeftJoin(string table)
        {
            return Join("LEFT JOIN ", table);
        }


        public SqlJoinClause Join(string table)
        {
            return Join("LEFT JOIN ", table);
        }


        public SqlJoinClause InnerJoin<T>(string asName = null) where T : EntityByType
        {
            return Join("INNER JOIN ", TableMaper.GetName<T>() + " " + asName);
        }

        public SqlJoinClause InnerJoinDB<T>(string dbName,string asName = null) where T : EntityByType
        { 
            return Join("INNER JOIN " + dbName + ".dbo.", TableMaper.GetName<T>() + " " + asName);
        }



        public SqlJoinClause LeftJoin<T>(string asName = null) where T : EntityByType
        {
            return Join("LEFT JOIN ", TableMaper.GetName<T>() + " " + asName);
        }


        public SqlJoinClause Join<T>(string asName = null) where T : EntityByType
        { 
            return Join("JOIN  ", TableMaper.GetName<T>() + " " + asName);
        }



        public class SqlJoinClause
        {
            private readonly Sql _sql;

            public SqlJoinClause(Sql sql)
            {
                _sql = sql;
            }

            public Sql On(string onClause, params object[] args)
            {
                return _sql.Append("ON " + onClause, args);
            }
        } 

    }
     
}
