using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DeliveryStreamCustomerWCF.Entities
{
    /// <summary>
    /// DeliveryRequest class
    /// </summary>
    [DataContract]
    public class DeliveryRequest
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
        /// Components
        /// Properties for Components datamember
        /// </summary>
        [DataMember]
        public List<DeliveryRequestItems> Items
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
        /// BOLWaitTime
        /// Properties for BOLWaitTime datamember
        /// </summary>
        [DataMember]
        public Boolean BOLWaitTime
        {
            get;
            set;
        }


        /// <summary>
        /// BOLWaitTimeTotal
        /// Properties for BOLWaitTimeTotal datamember
        /// </summary>
        [DataMember]
        public Decimal BOLWaitTimeTotal
        {
            get;
            set;
        }

        /// <summary>
        /// SiteWaitTime
        /// Properties for SiteWaitTime datamember
        /// </summary>
        [DataMember]
        public Boolean SiteWaitTime
        {
            get;
            set;
        }

        /// <summary>
        /// SiteWaitTime_Comment
        /// Properties for SiteWaitTime_Comment datamember
        /// </summary>
        [DataMember]
        public String SiteWaitTime_Comment
        {
            get;
            set;
        }

        /// <summary>
        /// SiteWaitTime_Start
        /// Properties for SiteWaitTime_Start datamember
        /// </summary>
        [DataMember]
        public DateTime? SiteWaitTime_Start
        {
            get;
            set;
        }

        /// <summary>
        /// SiteWaitTime_End
        /// Properties for SiteWaitTime_End datamember
        /// </summary>
        [DataMember]
        public DateTime? SiteWaitTime_End
        {
            get;
            set;
        }

        /// <summary>
        /// SplitLoad
        /// Properties for SplitLoad datamember
        /// </summary>
        [DataMember]
        public Boolean SplitLoad
        {
            get;
            set;
        }

        /// <summary>
        /// SplitLoad_Comment
        /// Properties for SplitLoad_Comment datamember
        /// </summary>
        [DataMember]
        public String SplitLoad_Comment
        {
            get;
            set;
        }

        /// <summary>
        /// SplitDrop
        /// Properties for SplitDrop datamember
        /// </summary>
        [DataMember]
        public Boolean SplitDrop
        {
            get;
            set;
        }


        /// <summary>
        /// SplitDrop_Comment
        /// Properties for SplitDrop_Comment datamember
        /// </summary>
        [DataMember]
        public String SplitDrop_Comment
        {
            get;
            set;
        }

        /// <summary>
        /// PumpOut
        /// Properties for PumpOut datamember
        /// </summary>
        [DataMember]
        public Boolean PumpOut
        {
            get;
            set;
        }


        /// <summary>
        /// PumpOut_Comment
        /// Properties for PumpOut_Comment datamember
        /// </summary>
        [DataMember]
        public String PumpOut_Comment
        {
            get;
            set;
        }

        /// <summary>
        /// Diversion
        /// Properties for Diversion datamember
        /// </summary>
        [DataMember]
        public Boolean Diversion
        {
            get;
            set;
        }

        /// <summary>
        /// Diversion_Comment
        /// Properties for Diversion_Comment datamember
        /// </summary>
        [DataMember]
        public String Diversion_Comment
        {
            get;
            set;
        }

        /// <summary>
        /// MinimumLoad
        /// Properties for MinimumLoad datamember
        /// </summary>
        [DataMember]
        public Boolean MinimumLoad
        {
            get;
            set;
        }

        /// <summary>
        /// MinimumLoad_Comment
        /// Properties for MinimumLoad_Comment datamember
        /// </summary>
        [DataMember]
        public String MinimumLoad_Comment
        {
            get;
            set;
        }


        /// <summary>
        /// Other
        /// Properties for Other datamember
        /// </summary>
        [DataMember]
        public Boolean Other
        {
            get;
            set;
        }

        /// <summary>
        /// Other_Comment
        /// Properties for Other_Comment datamember
        /// </summary>
        [DataMember]
        public String Other_Comment
        {
            get;
            set;
        }

        /// <summary>
        ///  Signature Status
        /// </summary>
        [DataMember]
        public String SignatureStatus
        {
            get;
            set;
        }
        
        /// <summary>
        ///  OrderLoadReviewEnabled Status
        /// </summary>
        [DataMember]
        public String OrderLoadReviewEnabled
        {
            get;
            set;
        }

        //2013.09.13 FSWW, Ramesh M Added ForCR#60123 Adding SignatureImage
        /// <summary>
        ///  SignatureImage
        /// </summary>
        [DataMember]
        public byte[] SignatureImage
        {
            get;
            set;
        }

        //2013.09.13 FSWW, Ramesh M Added ForCR#60123 Adding SignatureDate
        /// <summary>
        ///  SignatureDateTime
        /// </summary>
        [DataMember]
        public DateTime? SignatureDateTime
        {
            get;
            set;
        }

        

    }

    [DataContract]
    public class DeliveryRequestItems
    {

        /// <summary>
        /// SysLineNo
        /// Properties for SysLineNo datamember
        /// </summary>
        [DataMember]
        public Int32 SysLineNo
        {
            get;
            set;
        }

        /// <summary>
        /// ShipDocSysTrxNo
        /// Properties for ShipDocSysTrxNo datamember
        /// </summary>
        [DataMember]
        public Decimal ShipDocSysTrxNo
        {
            get;
            set;
        }

        /// <summary>
        /// ShipDocSysTrxLine
        /// Properties for ShipDocSysTrxLine datamember
        /// </summary>
        [DataMember]
        public Int32 ShipDocSysTrxLine
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
        /// DelivDtTm
        /// Properties for DelivDtTm datamember
        /// </summary>
        [DataMember]
        public DateTime DelivDtTm
        {
            get;
            set;
        }


        /// <summary>
        /// DeliveryQtyVarianceReason 
        /// Properties for DeliveryQtyVarianceReason  datamember
        /// </summary>
        [DataMember]
        public string DeliveryQtyVarianceReason 
        {
            get;
            set;
        }

        /// <summary>
        /// DeliveryImage
        /// Properties for DeliveryImage  datamember
        /// </summary>
        [DataMember]
        public byte[] DeliveryImage
        {
            get;
            set;
        }

        [DataMember]
        public byte[] DeliveryImagePdf
        {
            get;
            set;
        }

       [DataMember]
        public string BOLNo
        {
            get;
            set;
        }

       [DataMember]
       public Decimal BeforeVolume
       {
           get;
           set;
       }

       [DataMember]
       public Decimal AfterVolume
       {
           get;
           set;
       }

        [DataMember]
        public Guid OrderItemID
        {
            get;
            set;
        }
    }
}
