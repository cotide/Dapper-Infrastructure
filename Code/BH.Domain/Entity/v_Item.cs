using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace BH.Domain.Entity
{
	 	//v_Item
		public class v_Item
	{ 
      			/// <summary>
		/// ID
        /// </summary>		 
        public Guid ID { get;set;}     
				/// <summary>
		/// ItemTypeID
        /// </summary>		 
        public int ItemTypeID { get;set;}     
				/// <summary>
		/// ItemID
        /// </summary>		 
        public Guid ItemID { get;set;}     
				/// <summary>
		/// ItemName
        /// </summary>		 
        public string ItemName { get;set;}     
				/// <summary>
		/// ColorID
        /// </summary>		 
        public Guid ColorID { get;set;}     
				/// <summary>
		/// ColorName
        /// </summary>		 
        public string ColorName { get;set;}     
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

