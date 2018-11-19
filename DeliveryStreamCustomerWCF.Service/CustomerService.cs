// 2014.01.06 FSWW, Ramesh M Added VersionNo as input parameter in all methods For Versioning handling
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Activation;
using System.ServiceModel;
using DeliveryStreamCustomerWCF.DataAccess;
using DeliveryStreamCustomerWCF.Utils;
using DeliveryStreamCustomerWCF.Entities;
using System.Diagnostics;

namespace DeliveryStreamCustomerWCF.Service
{
    /// <summary>
    /// CustomerService class
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CustomerService : ServiceBase, ICustomerService
    {
        #region ICustomerService Members


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
        public void UpdateLoad(string companyID, string password, string loadNo, int vehicleID, int driverID, String VersionNo = "")
        {
            ISession session = null;
            try
            {
                ValidateCustomerLogin(companyID, password, VersionNo);
                session = GetSession();
                session.BeginTransaction();
                DALMethods.UpdateLoad(session, loadNo, vehicleID, driverID, VersionNo);
                session.CommitTransaction();
                CloseSession(session);
            }
            catch (ApplicationException ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                }
                throw ex;
            }
            catch (Exception ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                }
                Logging.LogError(ex);
                throw ex;
            }

        }

        /// <summary>
        /// UpdateLoadStatus
        /// Function to update the load status
        /// </summary>
        /// <param name="companyID">Company ID</param>
        /// <param name="password">Password</param>
        /// <param name="loadNo">Load number</param>
        /// <param name="loadStatus">Load Status</param>
        /// <param name="VersionNo">VersionNo</param>
        public void UpdateLoadStatus(string companyID, string password, string loadNo, string loadStatus, String VersionNo = "")
        {
            ISession session = null;
            try
            {
                ValidateCustomerLogin(companyID, password, VersionNo);
                session = GetSession();
                session.BeginTransaction();
                DALMethods.UpdateLoadStatus(session, loadNo, loadStatus, companyID, VersionNo);
                session.CommitTransaction();
                CloseSession(session);

            }
            catch (ApplicationException ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                }
                throw ex;
            }
            catch (Exception ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                }
                Logging.WriteLog("LoadNo: " + loadNo, System.Diagnostics.EventLogEntryType.Error);
                Logging.LogError(ex);
                throw ex;
            }
        }

        /// <summary>
        /// UpdateFreightBreakdown
        /// Function to update the FreightBreakdown details
        /// </summary>
        /// <param name="companyID">Company ID</param>
        /// <param name="password">Password</param>
        /// <param name="VersionNo">VersionNo</param>
        public void UpdateFreightBreakdown(string companyID, string password, Int32 SysTrxNo, Int32 FrtBrkdownline, char LegType, String OriginCity, String DestinationCity, String OriginState, String DestinationState, decimal CalcMiles, decimal ActualMiles, decimal OriginLat, decimal OriginLong, decimal DestLat, decimal DestLong, String VersionNo = "")
        {
            ISession session = null;
            try
            {
                ValidateCustomerLogin(companyID, password, VersionNo);
                session = GetSession();
                session.BeginTransaction();
                DALMethods.UpdateFreightBreakdown(session, companyID, SysTrxNo, FrtBrkdownline, LegType, OriginCity, DestinationCity, OriginState, DestinationState, CalcMiles, ActualMiles, OriginLat, OriginLong, DestLat, DestLong, VersionNo);
                session.CommitTransaction();
                CloseSession(session);

            }
            catch (ApplicationException ex)
            {
                if (session != null)
                {       
                    session.RollbackTransaction();
                    CloseSession(session);
                }
                throw ex;
            }
            catch (Exception ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                }
                Logging.LogError(ex);
                throw ex;
            }
        }

        /// <summary>
        /// UpdateOrderStatus
        /// Function to update the order status
        /// </summary>
        /// <param name="companyID">Company ID</param>
        /// <param name="password">Password</param>
        /// <param name="SysTrxNo">System transaction number</param>
        /// <param name="orderStatus">Order Status</param>
        /// <param name="VersionNo">VersionNo</param>
        public void UpdateOrderStatus(string companyID, string password, decimal SysTrxNo, string orderStatus, String VersionNo = "")
        {
            ISession session = null;
            try
            {
                ValidateCustomerLogin(companyID, password, VersionNo);
                session = GetSession();
                session.BeginTransaction();
                DALMethods.UpdateOrderStatus(session, SysTrxNo, orderStatus, companyID);
                session.CommitTransaction();
                CloseSession(session);
            }
            catch (ApplicationException ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                }
                throw ex;
            }
            catch (Exception ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                }
                Logging.LogError(ex);
                throw ex;
            }
        }

        /// <summary>
        /// UpdateShipment
        /// Function to update the shipment details
        /// </summary>
        /// <param name="companyID">Company ID</param>
        /// <param name="password">Password</param>
        /// <param name="shipmentReq">ShipmentRequest object</param>
        /// <returns>ShipmentResponse object</returns>
        /// <param name="VersionNo">VersionNo</param>
        public ShipmentResponse UpdateShipment(string companyID, string password, ShipmentRequest shipmentReq, String VersionNo = "")
        {
            ShipmentResponse shipmentResponse = null;
            ISession session = null;
            try
            {
                ValidateCustomerLogin(companyID, password, VersionNo);
                session = GetSession();
                session.BeginTransaction();
                shipmentResponse = DALMethods.UpdateShipment(session, shipmentReq, VersionNo);
                DALMethods.ClearShipmentErrors(shipmentReq.SysTrxNo, shipmentReq.SysTrxLine, session, VersionNo);
                session.CommitTransaction();
                CloseSession(session);
            }
            catch (ApplicationException ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                    session = GetSession();
                }
                //Logging.WriteToFile1("Dinesh 3- ");
                //Logging.WriteToFile1("Dinesh 4- ");
                DALMethods.UpdateShipmentErrors(shipmentReq, true, ex.StackTrace, ex.Message, session, VersionNo);
                throw ex;
            }
            catch (Exception ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                }
                //session = GetSession();
                //DALMethods.UpdateShipmentErrors(shipmentReq, true, ex.StackTrace, ex.Message, session, VersionNo);
                Logging.LogError(ex);
                //Logging.WriteToFile1("Dinesh 4- ");
                //shipmentResponse = new ShipmentResponse();
                //shipmentResponse.ErrorMessage = ex.Message + " - " + ex.StackTrace;
                //Logging.WriteToFile1("Dinesh 5- ");
                //return shipmentResponse;
                throw ex;
            }

            return shipmentResponse;
        }

        /// <summary>
        /// UpdateDeliveryDetails
        /// Function to update the delivery details
        /// </summary>
        /// <param name="companyID">Company ID</param>
        /// <param name="password">Password</param>
        /// <param name="deliveryReq">DeliveryRequest object</param>
        public Boolean UpdateDeliveryDetails(string companyID, string password, DeliveryRequest deliveryReq, String VersionNo = "")
        {
            ISession session = null;
            Boolean UpdDlvryDtls = false;
            try
            {
                ValidateCustomerLogin(companyID, password, VersionNo);
                session = GetSession();
                session.BeginTransaction();
                DALMethods.UpdateDeliveryDetails(session, deliveryReq, VersionNo);
                DALMethods.ClearDeliveryErrors(deliveryReq.SysTrxNo, session, VersionNo);
                session.CommitTransaction();
                CloseSession(session);
                UpdDlvryDtls = true;
            }
            catch (ApplicationException ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                }
                session = GetSession();
                DALMethods.UpdateDeliveryErrors(deliveryReq, true, ex.StackTrace, ex.Message, session, VersionNo);
                //throw ex;
                UpdDlvryDtls = false;
            }
            catch (Exception ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                    session = GetSession();
                }
                DALMethods.UpdateDeliveryErrors(deliveryReq, true, ex.StackTrace, ex.Message, session, VersionNo);
                Logging.LogError(ex);
                //throw ex;
                UpdDlvryDtls = false;
            }

            return UpdDlvryDtls;
        }


        /// <summary>
        /// Function to Update the BOLImage
        /// </summary>
        /// <param name="companyID">companyID</param>
        /// <param name="password">password</param>
        /// <param name="BOLImage">BOLImage</param>
        /// <param name="sysTrxNo">sysTrxNo</param>
        public void UpdateBOLImage(string companyID, string password, byte[] bolimage, byte[] bolimagePdf, int SystrxNo, string Bolno, String VersionNo = "")
        {
            ISession session = null;
            try
            {
                ValidateCustomerLogin(companyID, password, VersionNo);
                session = GetSession();
                session.BeginTransaction();
                DALMethods.UpdateBOLImage(session, bolimage,bolimagePdf, SystrxNo, Bolno, VersionNo);
                session.CommitTransaction();
                CloseSession(session);

            }
            catch (ApplicationException ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                }
                throw ex;
            }
            catch (Exception ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                }
                Logging.LogError(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Function to Update the Order PONumber
        /// </summary>
        /// <param name="companyID">companyID</param>
        /// <param name="password">password</param>
        /// <param name="BOLImage">OrderNo</param>
        /// <param name="sysTrxNo">SystrxNo</param>
        /// <param name="sysTrxNo">PoNo</param>
        public void UpdateOrderPONo(string companyID, string password, string OrderNo, int SystrxNo, string PONo, String VersionNo = "")
        {
            ISession session = null;
            try
            {
                ValidateCustomerLogin(companyID, password, VersionNo);
                session = GetSession();
                session.BeginTransaction();
                DALMethods.UpdatePONo(session, OrderNo, SystrxNo, PONo, VersionNo);
                session.CommitTransaction();
                CloseSession(session);
            }
            catch (ApplicationException ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                }
                throw ex;
            }
            catch (Exception ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                }
                Logging.LogError(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Function to Update the Undo ShipDoc
        /// </summary>
        /// <param name="companyID">companyID</param>
        /// <param name="password">password</param>
        /// <param name="sysTrxNo">SystrxNo</param>
        /// <param name="inStatus">inStatus</param>
        public void UndoShipDoc(string companyID, string password, int SystrxNo, char inStatus, String VersionNo = "")
        {
            ISession session = null;
            try
            {
                ValidateCustomerLogin(companyID, password, VersionNo);
                session = GetSession();
                session.BeginTransaction();
                DALMethods.UpdateUndoShipDoc(session, SystrxNo, inStatus, VersionNo);
                session.CommitTransaction();
                CloseSession(session);
            }
            catch (ApplicationException ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                }
                throw ex;
            }
            catch (Exception ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                }
                Logging.LogError(ex);
                throw ex;
            }
        }
        #endregion

        #region TankWagon Details

        public void InsertTWBOLS(string companyID, string password, TWBOLDetails twbolDetailsReq, TWBOLItemDetails twbolitemDetailsReq, String VersionNo = "")
        {

        }

        /// <summary>
        /// UpdateShipment
        /// Function to update the shipment details
        /// </summary>
        /// <param name="companyID">Company ID</param>
        /// <param name="password">Password</param>
        /// <param name="shipmentReq">ShipmentRequest object</param>
        /// <returns>ShipmentResponse object</returns>
        /// <param name="VersionNo">VersionNo</param>
        public String UpdateWagonShipment(string companyID, string password, TWBOLDetails shipmentReq, String VersionNo = "")
        {
            //ShipmentResponse shipmentResponse = null;
            string WagonShipment;
            ISession session = null;
            try
            {
                ValidateCustomerLogin(companyID, password, VersionNo);
                session = GetSession();
                session.BeginTransaction();

                WagonShipment = DALMethods.UpdateWagonShipment(session, shipmentReq, VersionNo);
                // DALMethods.ClearShipmentErrors(shipmentReq.SysTrxNo, shipmentReq.SysTrxLine, session, VersionNo);

                session.CommitTransaction();
                CloseSession(session);
            }
            catch (ApplicationException ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                    session = GetSession();
                }
                // DALMethods.UpdateShipmentErrors(shipmentReq, true, ex.StackTrace, ex.Message, session, VersionNo);
                throw ex;
            }
            catch (Exception ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                }
                session = GetSession();
                //DALMethods.UpdateShipmentErrors(shipmentReq, true, ex.StackTrace, ex.Message, session, VersionNo);
                Logging.LogError(ex);
                throw ex;
            }

            return WagonShipment;
        }

        /// <summary>
        /// EODInventoryProcess
        /// Function to Process the Inventory EOD
        /// </summary>
        /// <param name="companyID">Company ID</param>
        /// <param name="password">Password</param>
        /// <param name="shipmentReq">ShipmentRequest object</param>
        /// <returns>ShipmentResponse object</returns>
        /// <param name="VersionNo">VersionNo</param>
        public bool EODInventoryProcess(int inClientID, string password, string inToSite, string inSupplierCode, string inSupplyPtCode, string inProductCode, decimal inOrgQty, decimal inAvailableQty, decimal inNetQty, int inOrderSysTrxNo, int inOrderSysTrxLineNo, int invehicleID, int inDriverID, string inUserID, string inBOLNo, DateTime inBOLDtTm, string inBOLSessionID, string inOverShort, string VersionNo = "")
        {
            ISession session = null;
            try
            {
                ValidateCustomerLogin(inClientID.ToString(), password, VersionNo);
                session = GetSession();
                session.BeginTransaction();

                DALMethods.InsertEODInventoryProcess(session, inToSite, inSupplierCode, inSupplyPtCode, inProductCode, inOrgQty, inAvailableQty, inNetQty, inOrderSysTrxNo, inOrderSysTrxLineNo, invehicleID, inDriverID, inUserID, inBOLNo, inBOLDtTm, inBOLSessionID, inOverShort, inClientID);

                session.CommitTransaction();
                CloseSession(session);
            }
            catch (ApplicationException ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                    session = GetSession();
                }
                // DALMethods.UpdateShipmentErrors(shipmentReq, true, ex.StackTrace, ex.Message, session, VersionNo);
                throw ex;
            }
            catch (Exception ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                }
                session = GetSession();
                //DALMethods.UpdateShipmentErrors(shipmentReq, true, ex.StackTrace, ex.Message, session, VersionNo);
                Logging.LogError(ex);
                throw ex;
            }
            return true;
        }

        #endregion TankWagon Details

        #region RejectedLoads

        public void UpdateRejectedLoads(string loadNumber, string rejectedNote, string customerId)
        {
            ISession session = null;
            try
            {
                session = GetSession();
                DALMethods.InsertRejectedLoad(session, loadNumber, rejectedNote, customerId);
            }
            catch (Exception ex)
            {
                Logging.WriteLog(string.Format("UpdateRejectedLoads - Error - {0}", ex.Message), EventLogEntryType.Error);
            }
        }

        #endregion

        #region DeliveryNotes

        public void UpdateDeliveryNotes(string sysTrsNo, string deliveryNote)
        {
            ISession session = null;
            int isUpdated = 0;
            try
            {
                session = GetSession();
                isUpdated = DALMethods.UpdateDeliveryNotes(session, sysTrsNo, deliveryNote);
                Logging.WriteToFile1(string.Format("Delivery notes update for sysTrsNo = {0}", sysTrsNo));
            }
            catch (Exception ex)
            {
                Logging.WriteLog(string.Format("UpdateDeliveryNotes - Error - {0}", ex.Message), EventLogEntryType.Error);
            }            
        }


        #endregion

        /// <summary>
        /// UpdateBOLWaitTimeDetails
        /// Function to update the BOLWaitTime details
        /// </summary>
        /// <param name="companyID">Company ID</param>
        /// <param name="password">Password</param>
        /// <param name="VersionNo">VersionNo</param>
        public void UpdateBOLWaitTimeDetails(string companyID, string password, string ClientID, decimal SysTrxNo, string BOLNo, DateTime BOLWaitTimeStart, DateTime BOLWaitTimeEnd, string BOLWaitTimeComment, String VersionNo = "")
        {
            ISession session = null;
            try
            {
                ValidateCustomerLogin(companyID, password, VersionNo);
                session = GetSession();
                session.BeginTransaction();
                DALMethods.UpdateBOLWaitTimeDetails(session, ClientID, SysTrxNo, BOLNo, BOLWaitTimeStart, BOLWaitTimeEnd, BOLWaitTimeComment, VersionNo);
                session.CommitTransaction();
                CloseSession(session);
            }
            catch (ApplicationException ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                    session = GetSession();
                }
                throw ex;
            }
            catch (Exception ex)
            {
                if (session != null)
                {
                    session.RollbackTransaction();
                    CloseSession(session);
                }
                Logging.LogError(ex);
                throw ex;
            }
        }
    }
}