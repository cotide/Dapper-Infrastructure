#region << 版 本 注 释 >>
// ===============================================================================
// Project Name        :    BH.Domain.Entity.Base
// Project Description :    
// ===============================================================================
// Class Name          :    EntityWidthStringType
// Class Version       :    v1.0.0.0
// Class Description   :    
// Author              :    T00003028 - cotide
// Create Time         :    2016/12/27 17:31:02
// Update Time         :    2016/12/27 17:31:02
// ===============================================================================
// Copyright  VipShop 2016 . All rights reserved.
// ===============================================================================
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.Framework.Extensions;
using DapperInfrastructure.Extensions.Domain;
using DapperInfrastructure.Extensions.Helper;

namespace BH.Domain.Entity.Base
{
    public class EntityWidthStringType : EntityByType<string>
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public EntityWidthStringType()
        {
            // 默认初始化
            ID = Guid.NewGuid().ToId();
        }

        [Dapper.Contrib.Extensions.ExplicitKey]
        public override string ID { get; set; }
         
    }
}
