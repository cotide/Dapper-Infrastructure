using System;
using System.Collections.Generic;
using BH.Framework.Exceptions;
using BH.Framework.Extensions;

namespace BH.Framework.Utility
{
    /// <summary>
    /// 数据验证对象
    /// </summary>
    public class Guard
    {


        /// <summary>
        ///  检验参数合法性，数值类型不能小于0，引用类型不能为null，否则抛出相应异常
        /// </summary>
        /// <param name="arg"> 待检参数 </param>
        /// <param name="argName"> 待检参数名称 </param>
        /// <param name="canZero"> 数值类型是否可以等于0 </param>
        /// <exception cref="ComponentException" />
        public static void CheckArgument(object arg, string argName, bool canZero = false)
        {
            if (arg == null)
            {
                var e = new ArgumentNullException(argName);
                throw ThrowComponentException(string.Format("参数 {0} 为空引发异常。", argName), e);
            }
            Type type = arg.GetType();
            if (type.IsValueType && type.IsNumeric())
            {
                bool flag = !canZero ? arg.CastTo(0.0) <= 0.0 : arg.CastTo(0.0) < 0.0;
                if (flag)
                {
                    var e = new ArgumentOutOfRangeException(argName);
                    throw ThrowComponentException(string.Format("参数 {0} 不在有效范围内引发异常。具体信息请查看系统日志。", argName), e);
                }
            }
            if (type == typeof(Guid) && (Guid)arg == Guid.Empty)
            {
                var e = new ArgumentNullException(argName);
                throw ThrowComponentException(string.Format("参数{0}为空Guid引发异常。", argName), e);
            }
        }

        /// <summary>
        /// 向调用层抛出组件异常
        /// </summary>
        /// <param name="msg"> 自定义异常消息 </param>
        /// <param name="e"> 实际引发异常的异常实例 </param>
        public static ComponentException ThrowComponentException(string msg, Exception e = null)
        {
            if (string.IsNullOrEmpty(msg) && e != null)
            {
                msg = e.Message;
            }
            else if (string.IsNullOrEmpty(msg))
            {
                msg = "未知组件异常，详情请查看日志信息。";
            }
            return e == null ? new ComponentException(string.Format("组件异常：{0}", msg)) : new ComponentException(string.Format("组件异常：{0}", msg), e);
        }

        /// <summary>
        ///     向调用层抛出数据访问层异常
        /// </summary>
        /// <param name="msg"> 自定义异常消息 </param>
        /// <param name="e"> 实际引发异常的异常实例 </param>
        public static DataAccessException ThrowDataAccessException(string msg, Exception e = null)
        {
            if (string.IsNullOrEmpty(msg) && e != null)
            {
                msg = e.Message;
            }
            else if (string.IsNullOrEmpty(msg))
            {
                msg = "未知数据访问层异常，详情请查看日志信息。";
            }
            return e == null
                ? new DataAccessException(string.Format("数据访问层异常：{0}", msg))
                : new DataAccessException(string.Format("数据访问层异常：{0}", msg), e);
        }


        /// <summary>
        ///  向调用层抛出数据访问层异常
        /// </summary>
        /// <param name="msg"> 自定义异常消息 </param>
        /// <param name="e"> 实际引发异常的异常实例 </param>
        public static BusinessException ThrowBusinessException(string msg, Exception e = null)
        {
            if (string.IsNullOrEmpty(msg) && e != null)
            {
                msg = e.Message;
            }
            else if (string.IsNullOrEmpty(msg))
            {
                msg = "未知业务逻辑层异常，详情请查看日志信息。";
            }
            return e == null
                ?
                new BusinessException(string.Format("业务逻辑层异常：{0}", msg))
                :
                new BusinessException(string.Format("业务逻辑层异常：{0}", msg), e);
        }

        /// <summary>
        /// 验证是否为NULL
        /// </summary>
        /// <param name = "parameter">验证值</param>
        /// <param name = "parameterName">参数名</param>
        /// <param name="title">标题</param>
        /// <param name="msg">错误信息</param>
        public static void IsNotNull(
            object parameter,
            string parameterName,
            string title = null,
            string msg = null)
        {
            if (parameter == null)
            {
                throw !string.IsNullOrEmpty(msg)
                    ? new ArgumentNullException( 
                        string.Format("{0}：{1}", string.IsNullOrEmpty(title) ? "数据验证错误：" : title, msg))
                          : new ArgumentNullException( string.Format("{0}：{1} Is Not NULL ", 
                                                      string.IsNullOrEmpty(title) ? "数据验证错误：" : title, parameterName));
            }
        }

        /// <summary>
        /// 验证是否为NULL
        /// </summary>
        /// <param name = "parameter">验证值</param>
        /// <param name = "parameterName">参数名</param> 
        /// <param name="title">标题</param>
        /// <param name="msg">错误信息</param>
        public static void IsNotNullOrEmpty(
            string parameter,
            string parameterName,
            string title = null,
            string msg = null)
        {
            if (string.IsNullOrEmpty((parameter ?? string.Empty)))
            {
                throw !string.IsNullOrEmpty(msg)
                     ? new ArgumentNullException(
                         string.Format("{0}：{1}", string.IsNullOrEmpty(title) ? "数据验证错误：" : title, msg))
                           : new ArgumentNullException(string.Format("{0}：{1} Is Not NULL ",
                                                       string.IsNullOrEmpty(title) ? "数据验证错误：" : title, parameterName));
            }
        }

        /// <summary>
        /// 验证是否为NULL
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "parameter">验证值</param>
        /// <param name = "parameterName">参数名</param>
        /// <param name="title">标题</param>
        /// <param name="msg">错误信息</param>
        public static void IsNotNullOrEmpty<T>(
            T[] parameter,
            string parameterName,
            string title = null,
            string msg = null)
        {
            IsNotNull(parameter, parameterName);

            if (parameter.Length == 0)
            {
                throw !string.IsNullOrEmpty(msg)
                     ? new ArgumentNullException(
                         string.Format("{0}：{1}", string.IsNullOrEmpty(title) ? "数据验证错误：" : title, msg))
                           : new ArgumentNullException(string.Format("{0}：{1} Is Not NULL ",
                                                       string.IsNullOrEmpty(title) ? "数据验证错误：" : title, parameterName));
            }
        }

        /// <summary>
        /// 验证是否为NULL
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "parameter">验证值</param>
        /// <param name = "parameterName">参数名</param>
        /// <param name="title">标题</param>
        /// <param name="msg">错误信息</param>
        public static void IsNotNullOrEmpty<T>(
            ICollection<T> parameter,
            string parameterName,
            string title = null,
            string msg = null)
        {
            IsNotNull(parameter, parameterName);

            if (parameter.Count == 0)
            {
                throw !string.IsNullOrEmpty(msg)
                     ? new ArgumentNullException(
                         string.Format("{0}：{1}", string.IsNullOrEmpty(title) ? "数据验证错误：" : title, msg))
                           : new ArgumentNullException(string.Format("{0}：{1} Is Not NULL ",
                                                       string.IsNullOrEmpty(title) ? "数据验证错误：" : title, parameterName));
            }
        }


        /// <summary>
        ///  验证是否大于0
        /// </summary>
        /// <param name = "parameter">验证值</param>
        /// <param name = "parameterName">参数名</param>
        /// <param name="title">标题</param>
        /// <param name="msg">错误信息</param>
        public static void IsNotZeroOrNegative(
            int parameter,
            string parameterName,
            string title = null,
            string msg = null)
        {
            if (parameter <= 0)
            {
                throw !string.IsNullOrEmpty(msg)
                  ? new ArgumentNullException(
                      string.Format("{0}：{1}", string.IsNullOrEmpty(title) ? "数据验证错误：" : title, msg))
                        : new ArgumentNullException(string.Format("{0}：{1}  Is Not Positive Integer ",
                                                    string.IsNullOrEmpty(title) ? "数据验证错误：" : title, parameterName));
            }
        }

    }
}
