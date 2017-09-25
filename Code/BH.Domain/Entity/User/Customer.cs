#region << 版 本 注 释 >>
// ===============================================================================
// Project Name        :    BH.Domain.Entity.User
// Project Description :    
// ===============================================================================
// Class Name          :    Customer
// Class Version       :    v1.0.0.0
// Class Description   :    
// Author              :    cotide
// Create Time         :    2016/12/27 15:38:36
// Update Time         :    2016/12/27 15:38:36
// ===============================================================================
// Copyright  VipShop 2016 . All rights reserved.
// ===============================================================================
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.Domain.Entity.Base;

namespace BH.Domain.Entity.User
{
    /// <summary>
    /// 客户
    /// </summary>
    public  class Customer : EntityWidthGuidType 
    {

        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }


        /// <summary>
        /// 汉子编号
        /// </summary>
        public string ChineseCode { get; set; }


        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// 客户
        /// </summary>
        public CustomerInfo CustomerInfo { get; set; }
         
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsEffective { get; set; }
    }
}
