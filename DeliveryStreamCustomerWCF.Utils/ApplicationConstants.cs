using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliveryStreamCustomerWCF.Utils
{
    /// <summary>
    /// ApplicationConstants class
    /// </summary>
    public static class ApplicationConstants
    {
        public struct Encription
        {
            public const String siv = "A445ABDSADQW6A7DFDHAS3JH9HTA2DFJ";
            public const String EncryptionKey = "ASDFG5RFGN234LMVAO3049ADF34NAASDF0345234MLASDFNASDFN3KLN34NASDFN";
        }

        public struct Connection
        {
            public const String ConnectionString = "DeliveryStreamCustomer";
        }

        public struct ErrorStatusCode
        { 
            public const String Error_In_Shipment = "X";
            public const String Error_In_Delivery = "Y";
        }

        public struct Errors
        {
            public const String FunctionError = "Error occured. Function : in {0}, Class : {1}, Error Message : {2} Stack Trace : {3}";
            public const String ConnectionString = "Could not find {0} connection string in config file";
            public const String InvalidCustomerCreadentials = "Unable to validate creadentails. Please check CustomerID and try again.";            
        }

        public struct Logging
        {
            public const String Log = "DeliveryStream";
            public const String Source = "CustomerWCF";
        }
    }
}
