using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace BH.Domain.Entity
{
	 	//v_ItemUMConversion
		public class v_ItemUMConversion
	{ 
      			/// <summary>
		/// ItemTypeID
        /// </summary>		 
        public int ItemTypeID { get;set;}     
				/// <summary>
		/// ItemID
        /// </summary>		 
        public Guid ItemID { get;set;}     
				/// <summary>
		/// SourceUMID
        /// </summary>		 
        public Guid SourceUMID { get;set;}     
				/// <summary>
		/// TargetUMID
        /// </summary>		 
        public Guid TargetUMID { get;set;}     
				/// <summary>
		/// ConversionValue
        /// </summary>		 
        public decimal ConversionValue { get;set;}     
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

