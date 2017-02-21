using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using BH.Domain.Entity.Base;
namespace BH.Domain.Entity
{
		
	/// <summary>
    /// CategorySerialNumber
    /// </summary>
    public class CategorySerialNumber : EntityWidthGuidType 
	{ 
      	
        /// <summary>
        /// CategoryID
        /// </summary>		 
        public Guid CategoryID { get;set;}     
			  
        /// <summary>
        /// SerialNrLength
        /// </summary>		 
        public int SerialNrLength { get;set;}     
			  
        /// <summary>
        /// NextNr
        /// </summary>		 
        public int NextNr { get;set;}     
			   
	}
}

