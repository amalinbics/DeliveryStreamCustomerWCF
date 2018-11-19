using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DeliveryStreamCustomerWCF.Entities
{
    /// <summary>
    /// TWBOLDetails class
    /// </summary>
    [DataContract]
    public class TWBOLDetails
    {
        /// <summary>
        /// ClientID
        /// Properties for ClientID datamember
        /// </summary>
        [DataMember]
        public Guid ClientID
        {
            get;
            set;
        }

        /// <summary>
        /// BOLHdrID
        /// Properties for BOLHdrID datamember
        /// </summary>
        [DataMember]
        public Guid BOLHdrID
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
        /// SupplierCode
        /// Properties for SupplierCode datamember
        /// </summary>
        [DataMember]
        public String SupplierCode
        {
            get;
            set;
        }

        /// <summary>
        /// SupplyPointCode
        /// Properties for SupplyPointCode datamember
        /// </summary>
        [DataMember]
        public String SupplyPointCode
        {
            get;
            set;
        }

        /// <summary>
        /// UpdatedBy
        /// Properties for UpdatedBy datamember
        /// </summary>
        [DataMember]
        public Int32 UpdatedBy
        {
            get;
            set;
        }

        /// <summary>
        /// BOLItemDetails
        /// Properties for BOLItemDetails datamember
        /// </summary>
        [DataMember]
        public List<TWBOLItemDetails> BOLItemDetails
        {
            get;
            set;
        }
    }
    /// <summary>
    /// TWBOLItemDetails class
    /// </summary>
    /// 
    [DataContract]
    public class TWBOLItemDetails
    {
        /// <summary>
        /// SysTrxNo
        /// Properties for SysTrxNo datamember
        /// </summary>
        [DataMember]
        public Decimal SystrxNo
        {
            get;
            set;
        }

        /// <summary>
        /// SystrxLine
        /// Properties for SystrxLine datamember
        /// </summary>
        [DataMember]
        public Int32 SystrxLine
        {
            get;
            set;
        }

        /// <summary>
        /// CompartmentID
        /// Properties for CompartmentID datamember
        /// </summary>
        [DataMember]
        public Int32 CompartmentID
        {
            get;
            set;
        }

        /// <summary>
        /// ProdCode
        /// Properties for ProdCode datamember
        /// </summary>
        [DataMember]
        public String ProdCode
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
        /// OrderedQty
        /// Properties for OrderedQty datamember
        /// </summary>
        [DataMember]
        public Decimal OrderedQty
        {
            get;
            set;
        }

        /// <summary>
        /// Notes
        /// Properties for Notes datamember
        /// </summary>
        [DataMember]
        public String Notes
        {
            get;
            set;
        }
    }
}
