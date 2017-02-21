#region << 版 本 注 释 >>
// ===============================================================================
// Project Name        :    BH.Domain.Entity.User
// Project Description :    
// ===============================================================================
// Class Name          :    CustomerInfo
// Class Version       :    v1.0.0.0
// Class Description   :    
// Author              :    T00003028 - cotide
// Create Time         :    2016/12/27 15:38:44
// Update Time         :    2016/12/27 15:38:44
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
    /// 客户信息
    /// </summary>
    public class CustomerInfo : EntityWidthGuidType
    {

       

        /// <summary>
        /// 
        /// </summary>
        public string TaxNr { get; set; }


        /// <summary>
        ///  区域编码
        /// </summary>
        public string AreaCode { get; set; }


        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsEffective { get; set; }


        /// <summary>
        /// 服务开始时间
        /// </summary>
        public DateTime EffectiveStart { get; set; }


        /// <summary>
        /// 服务结束时间
        /// </summary>
        public DateTime EffectiveEnd { get; set; }

    }
}
