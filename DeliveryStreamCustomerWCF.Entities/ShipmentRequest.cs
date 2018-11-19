//2014.01.28  Ramesh M Added For Support Multi BOL ites added ExtSysTrxLine For CR#62038
//2014.03.18  Ramesh M Added For CR#62719 added  TrailerCode in input parameters
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DeliveryStreamCustomerWCF.Entities
{
    /// <summary>
    /// ShipmentRequest class
    /// </summary>
    [DataContract]
    public class ShipmentRequest
    {
        /// <summary>
        /// SysTrxNo
        /// Properties for SysTrxNo datamember
        /// </summary>
        [DataMember]
        public Decimal SysTrxNo
        {
            get;
            set;
        }

        /// <summary>
        /// SysTrxLine
        /// Properties for SysTrxLine datamember
        /// </summary>
        [DataMember]
        public Int32 SysTrxLine
        {
            get;
            set;
        }

        /// <summary>
        /// Components
        /// Properties for Components datamember
        /// </summary>
        [DataMember]
        public List<ShipmentRequestComponents> Components
        {
            get;
            set;
        }

        /// <summary>
        /// UserID
        /// Properties for UserID datamember
        /// </summary>
        [DataMember]
        public String UserID
        {
            get;
            set;
        }

        /// <summary>
        /// OrderLoadReviewEnabled
        /// Properties for OrderLoadReviewEnabled datamember
        /// </summary>
        [DataMember]
        public string OrderLoadReviewEnabled
        {
            get;
            set;
        }
        /// <summary>
        /// BOLImage
        /// Properties for BOLImage datamember
        /// </summary>
        [DataMember]
        public Byte?[] BOLImage
        {
            get;
            set;
        }
      
        //2014.01.28  Ramesh M Added For Support Multi BOL ites added ExtSysTrxLine For CR#62038
        [DataMember]
        public string SupplierCode
        {
            get;
            set;
        }
        [DataMember]
        public string SupplyPointCode
        {
            get;
            set;
        }     
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public Int32 ExtSysTrxLine
        {
            get;
            set;
        }
        // 2014.03.18  Ramesh M Added For CR#62719 added  TrailerCode in input parameters
        [DataMember]
        public String TrailerCode
        {
            get;
            set;
        }
    }

    [DataContract]
    public class ShipmentRequestComponents
    {
        
        /// <summary>
        /// ComponentNo
        /// Properties for ComponentNo datamember
        /// </summary>
        [DataMember]
        public Int32 ComponentNo
        {
            get;
            set;
        }

        /// <summary>
        /// GrossQty
        /// Properties for GrossQty datamember
        /// </summary>
        [DataMember]
        public Decimal GrossQty
        {
            get;
            set;
        }

        /// <summary>
        /// NetQty
        /// Properties for NetQty datamember
        /// </summary>
        [DataMember]
        public Decimal NetQty
        {
            get;
            set;
        }

        /// <summary>
        /// BOLNo
        /// Properties for BOLNo datamember
        /// </summary>
        [DataMember]
        public String BOLNo
        {
            get;
            set;
        }

        ///// <summary>
        ///// Image
        ///// Properties for Image datamember
        ///// </summary>
        //[DataMember]
        //public Byte[] Image
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// BOLDateTime
        /// Properties for BOLDateTime datamember
        /// </summary>
        [DataMember]
        public DateTime BOLDateTime
        {
            get;
            set;
        }

        /// <summary>
        /// BOLEndDateTime
        /// Properties for BOLEndDateTime datamember
        /// </summary>
        [DataMember]
        public DateTime BOLEndDateTime
        {
            get;
            set;
        }        

        /// <summary>
        /// BOLQtyVarianceReason 
        /// Properties for BOLQtyVarianceReason  datamember
        /// </summary>
        [DataMember]
        public string BOLQtyVarianceReason 
        {
            get;
            set;
        }
        /// <summary>
        /// BOLImage
        /// Properties for BOLImage datamember
        /// </summary>
        [DataMember]
        public Byte[] BOLImage
        {
            get;
            set;
        }
        //2014.01.28  Ramesh M Added For Support Multi BOL ites added ExtSysTrxLine For CR#62038
        [DataMember]
        public string SupplierCode
        {
            get;
            set;
        }
        [DataMember]
        public string SupplyPointCode
        {
            get;
            set;
        }       
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public Int32 ExtSysTrxLine
        {
            get;
            set;
        }
        // 2014.03.18  Ramesh M Added For CR#62719 added  TrailerCode in input parameters
        [DataMember]
        public String TrailerCode
        {
            get;
            set;
        }
    }

}
