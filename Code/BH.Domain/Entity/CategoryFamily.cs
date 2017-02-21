using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using BH.Domain.Entity.Base;
namespace BH.Domain.Entity
{
		
	/// <summary>
    /// CategoryFamily
    /// </summary>
    public class CategoryFamily : EntityWidthGuidType 
	{ 
      	
        /// <summary>
        /// Code
        /// </summary>		 
        public string Code { get;set;}     
			  
        /// <summary>
        /// Name
        /// </summary>		 
        public string Name { get;set;}     
			  
        /// <summary>
        /// PrefixCode
        /// </summary>		 
        public string PrefixCode { get;set;}     
			  
        /// <summary>
        /// IsLast
        /// </summary>		 
        public bool IsLast { get;set;}     
			  
        /// <summary>
        /// ParentID
        /// </summary>		 
        public Guid ParentID { get;set;}     
			  
        /// <summary>
        /// CategoryApplicationID
        /// </summary>		 
        public int CategoryApplicationID { get;set;}     
			  
        /// <summary>
        /// OrderNr
        /// </summary>		 
        public int OrderNr { get;set;}     
			  
        /// <summary>
        /// Description
        /// </summary>		 
        public string Description { get;set;}     
			  
        /// <summary>
        /// IsEffective
        /// </summary>		 
        public bool IsEffective { get;set;}     
			  
        /// <summary>
        /// EffectiveStart
        /// </summary>		 
        public DateTime EffectiveStart { get;set;}     
			  
        /// <summary>
        /// EffectiveEnd
        /// </summary>		 
        public DateTime EffectiveEnd { get;set;}     
			   
	}
}

