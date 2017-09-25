#region << 版 本 注 释 >>
// ===============================================================================
// Project Name        :    BH.Domain.Entity.Test
// Project Description :    
// ===============================================================================
// Class Name          :    Test1Type
// Class Version       :    v1.0.0.0
// Class Description   :    
// Author              :    T00003028 - cotide
// Create Time         :    2016/12/27 15:54:11
// Update Time         :    2016/12/27 15:54:11
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
using System.Data.Linq.Mapping;

namespace BH.Domain.Entity.Test
{
 
    public class Test1Type : EntityWidthStringType 
    { 
        public string TypeName { get; set; }
 

        public string TestId { get; set; }
         
    }
}
