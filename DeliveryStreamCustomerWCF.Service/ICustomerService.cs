using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using DeliveryStreamCustomerWCF.Utils;
using DeliveryStreamCustomerWCF.DataAccess;
using DeliveryStreamCustomerWCF.Entities;

namespace DeliveryStreamCustomerWCF.Service
{
    /// <summary>
    /// Interface ICloudService
    /// </summary>
    [ServiceContract]
    public interface ICustomerService
    {   
        #region Update details

        /// <summary>
        /// UpdateLoad
        /// Function to update the load records
        /// </summary>
        /// <param name="companyID">Company ID</param>
        /// <param name="password">Password</param>
        /// <param name="loadNo">Load number</param>
        /// <param name="vehicleID">Vehicle ID</param>
        /// <param name="driverID">Driver ID</param>
        /// <param name="VersionNo">VersionNo</param>
        [OperationContract]
        void UpdateLoad(String companyID, String password, String loadNo, Int32 vehicleID, Int32 driverID, String VersionNo = "");

        /// <summary>
        /// UpdateLoadStatus
        /// Function to update the load status
        /// </summary>
        /// <param name="companyID">Company ID</param>
        /// <param name="password">Password</param>
        /// <param name="loadNo">Load number</param>
        /// <param name="loadStatus">Load Status</param>
        /// <param name="VersionNo">VersionNo</param>
        [OperationContract]
        void UpdateLoadStatus(String companyID, String password, String loadNo, String loadStatus,String VersionNo = "");

        /// <summary>
        /// UpdateOrderStatus
        /// Function to update the order status
        /// </summary>
        /// <param name="companyID">Company ID</param>
        /// <param name="password">Password</param>
        /// <param name="SysTrxNo">System transaction number</param>
        /// <param name="orderStatus">Order Status</param>
        /// <param name="VersionNo">VersionNo</param>
        [OperationContract]
        void UpdateOrderStatus(String companyID, String password, Decimal SysTrxNo, String orderStatus, String VersionNo = "");

        /// <summary>
        /// UpdateShipment
        /// Function to update the shipment details
        /// </summary>
        /// <param name="companyID">Company ID</param>
        /// <param name="password">Password</param>
        /// <param name="shipmentReq">ShipmentRequest object</param>
        /// <returns>ShipmentResponse object</returns>
        /// <param name="VersionNo">VersionNo</param>
        [OperationContract]
        ShipmentResponse UpdateShipment(String companyID, String password, ShipmentRequest shipmentReq, String VersionNo = "");

        /// <summary>
        /// UpdateDeliveryDetails
        /// Function to update the delivery details
        /// </summary>
        /// <param name="companyID">Company ID</param>
        /// <param name="password">Password</param>
        /// <param name="deliveryReq">DeliveryRequest object</param>
        /// <param name="VersionNo">VersionNo</param>
        [OperationContract]
        Boolean UpdateDeliveryDetails(String companyID, String password, DeliveryRequest deliveryReq, String VersionNo = "");

        [OperationContract]
        void InsertTWBOLS(string companyID, string password, TWBOLDetails twbolDetailsReq, TWBOLItemDetails twbolitemDetailsReq, String VersionNo = "");

        [OperationContract]
        String UpdateWagonShipment(string companyID, string password, TWBOLDetails shipmentReq, String VersionNo = "");

        [OperationContract]
        bool EODInventoryProcess(int inClientID, string password, string inToSite, string inSupplierCode, string inSupplyPtCode, string inProductCode, decimal inOrgQty, decimal inAvailableQty, decimal inNetQty, int inOrderSysTrxNo, int inOrderSysTrxLineNo, int invehicleID, int inDriverID, string inUserID, string inBOLNo, DateTime inBOLDtTm, string inBOLSessionID, string inOverShort, string VersionNo = "");

        [OperationContract]
        void UpdateFreightBreakdown(string companyID, string password, Int32 SysTrxNo, Int32 FrtBrkdownline, char LegType, String OriginCity, String DestinationCity, String OriginState, String DestinationState, decimal CalcMiles, decimal ActualMiles, decimal OriginLat, decimal OriginLong, decimal DestLat, decimal DestLong, String VersionNo = "");

        /// <summary>
        /// UpdateBOLImage
        /// Function to update the BOLImage
        /// </summary>
        [OperationContract]
        void UpdateBOLImage(string companyID, string password, byte[] bolimage,byte[] BOLImagePdf, int SystrxNo, string Bolno, String VersionNo = "");

        /// <summary>
        /// UpdateOrderPONo
        /// Function to update the PONo
        /// </summary>
        [OperationContract]
        void UpdateOrderPONo(string companyID, string password, string OrderNo, int SystrxNo, string PONo, String VersionNo = "");

        [OperationContract]
        void UndoShipDoc(string companyID, string password, int SystrxNo, char inStatus, String VersionNo = "");

        [OperationContract]
        void UpdateRejectedLoads(string loadNumber, string rejectedNote, string customerId);

        [OperationContract]
        void UpdateDeliveryNotes(string sysTrsNo, string deliveryNotes);

        #endregion Update details

        [OperationContract]
        void UpdateBOLWaitTimeDetails(string companyID, string password, string ClientID, decimal SysTrxNo, string BOLNo, DateTime BOLWaitTimeStart, DateTime BOLWaitTimeEnd, string BOLWaitTimeComment, String VersionNo = "");
    }
}
