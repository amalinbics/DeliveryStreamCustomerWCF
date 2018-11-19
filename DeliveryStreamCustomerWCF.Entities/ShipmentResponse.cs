using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DeliveryStreamCustomerWCF.Entities
{
    /// <summary>
    /// ShipmentResponse class
    /// </summary>
    [DataContract]
    public class ShipmentResponse
    {
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
        /// OrderLoadReviewEnabled
        /// Properties for OrderLoadReviewEnabled datamember
        /// </summary>
        [DataMember]
        public Boolean OrderLoadReviewEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// ErrorMessage
        /// Properties for ErrorMessage datamember
        /// </summary>
        [DataMember]
        public String ErrorMessage
        {
            get;
            set;
        }
    }
}
